using System;
using System.Collections;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Deuteros
{
	/// <summary>
	/// This class is used as repository of textures.
	/// </summary>
	public class TextureManager
	{
		private ArrayList textures;
		private Stack unloadedTextures;

		/// <summary>
		/// Creates new TextureManager instance.
		/// </summary>
		public TextureManager()
		{
			textures = new ArrayList();
			unloadedTextures = new Stack();
		}

		/// <summary>
		/// Gets the texture at index, if available
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public TextureManagerItem GetTexture(int index)
		{
			if ( index == -1 || index >= textures.Count ) return null;

			if (textures[index] == null)
			{
				// textura uz byla vypustena
				return null;
			}
			else
			{
				return (TextureManagerItem)textures[index];
			}
		}

		/// <summary>
		/// Returns the index of specified texture.
		/// </summary>
		/// <param name="FileName">Path to texture file. If not loaded yet, loads the texture.</param>
		/// <returns>Index of texture</returns>
		public int AddTexture( string FileName )
		{
			int idx = -1;
			for ( int i = 0; i < textures.Count; i ++ )
			{
				if ( textures[i] != null )
				{
					if ( ((TextureManagerItem)textures[i]).Filename == FileName ) // nalezen
					{
						idx = i;

						break;
					}
				}
			}

			if ( idx == -1 ) // nenalezen, zalozime
			{
				TextureManagerItem it = new TextureManagerItem(FileName);
				if ( it != null )
				{
					if ( unloadedTextures.Count == 0 )
					{
						// pridame na konec
						textures.Add(it);
						idx = textures.Count - 1;
					}
					else
					{
						// zalozime na predchozim pouzitem miste
						textures[(int)unloadedTextures.Peek()] = it;
						idx = (int)unloadedTextures.Pop();
					}
				}
			}

			if ( idx != -1 )
			{
				GetTexture(idx).NewUser();
			}

			return idx;
		}

		/// <summary>
		/// Unloads the texture if last user.
		/// </summary>
		/// <param name="Index">Index of texture</param>
		public void RemoveTexture( int Index )
		{
			if ( Index == -1 ) return;

			if ( textures[Index] != null )
			{
				GetTexture(Index).LostUser();

				if ( GetTexture(Index).Users == 0 )
				{
					GetTexture(Index).Dispose();
					textures[Index] = null;
					unloadedTextures.Push(Index);
				}
			}
		}

		/// <summary>
		/// Unloads all textures. Take caution as it may cause serious damage.
		/// </summary>
		public void Clear()
		{
			for ( int i = 0; i < textures.Count; i ++ )
			{
				TextureManagerItem it = GetTexture(i);

				if ( it != null )
				{
					it.Texture.Dispose();
					textures[i] = null;
				}
			}

			textures.Clear();
		}
	}

	/// <summary>
	/// TextureManager item
	/// </summary>
	public class TextureManagerItem
	{
		private string fileName = "";
		private int users = 0;
		private Texture texture = null;

		/// <summary>
		/// Gets the path of texture file.
		/// </summary>
		public string Filename { get { return fileName; } }
		/// <summary>
		/// Gets the number of users of this texture.
		/// </summary>
		public int Users { get { return users; } }
		/// <summary>
		/// Gets the associated texture
		/// </summary>
		public Texture Texture { get { return texture; } }

		/// <summary>
		/// Create new TextureManagerItem instance and load the texture file.
		/// </summary>
		/// <param name="FileName"></param>
		public TextureManagerItem(string FileName)
		{
			this.fileName = FileName;

			try
			{
				this.texture = TextureLoader.FromFile(GameEngine.Instance.RenderEngine.Device, FileName);
			}
			catch
			{}
		}
		
		/// <summary>
		/// Accounts new user of this texture
		/// </summary>
		public void NewUser()
		{
			this.users++;
		}
		/// <summary>
		/// Accounts loss of user of this texture
		/// </summary>
		public void LostUser()
		{
			this.users--;
		}

		/// <summary>
		/// Disposes the item
		/// </summary>
		public void Dispose()
		{
			if ( this.texture != null )
			{
				this.texture.Dispose();
				this.texture = null;
			}
		}
	}
}
