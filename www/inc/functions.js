
    //  Deuteros X website
    //  Copyright (C) 2008 Geddeth aka Achton Smidt Netherclift <achton@netherclift.net>
    //
    //  This file is part of Deuteros X website.

    //  Deuteros X website is free software: you can redistribute it and/or modify
    //  it under the terms of the GNU General Public License as published by
    //  the Free Software Foundation, either version 3 of the License, or
    //  (at your option) any later version.

    //  Deuteros X website is distributed in the hope that it will be useful,
    //  but WITHOUT ANY WARRANTY; without even the implied warranty of
    //  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    //  GNU General Public License for more details.

    //  You should have received a copy of the GNU General Public License
    //  along with Deuteros X website.  If not, see <http://www.gnu.org/licenses/>.


    function prepareImageSwap(elem,mouseOver,mouseOutRestore,mouseDown,mouseUpRestore,mouseOut,mouseUp) {
    //Do not delete these comments.
    //Non-Obtrusive Image Swap Script V1.1 by Hesido.com
    //Attribution required on all accounts
        if (typeof(elem) == 'string') elem = document.getElementById(elem);
        if (elem == null) return;
        var regg = /(.*)(_nm\.)([^\.]{3,4})$/
        var prel = new Array(), img, imgList, imgsrc, mtchd;
        imgList = elem.getElementsByTagName('img');
        for (var i=0; img = imgList[i]; i++) {
            if (!img.rolloverSet && img.src.match(regg)) {
                mtchd = img.src.match(regg);
                img.hoverSRC = mtchd[1]+'_hv.'+ mtchd[3];
                img.outSRC = img.src;
                if (typeof(mouseOver) != 'undefined') {
                    img.hoverSRC = (mouseOver) ? mtchd[1]+'_hv.'+ mtchd[3] : false;
                    img.outSRC = (mouseOut) ? mtchd[1]+'_ou.'+ mtchd[3] : (mouseOver && mouseOutRestore) ? img.src : false;
                    img.mdownSRC = (mouseDown) ? mtchd[1]+'_md.' + mtchd[3] : false;
                    img.mupSRC = (mouseUp) ? mtchd[1]+'_mu.' + mtchd[3] : (mouseOver && mouseDown && mouseUpRestore) ? img.hoverSRC : (mouseDown && mouseUpRestore) ? img.src : false;
                    }
                if (img.hoverSRC) {preLoadImg(img.hoverSRC); img.onmouseover = imgHoverSwap;}
                if (img.outSRC) {preLoadImg(img.outSRC); img.onmouseout = imgOutSwap;}
                if (img.mdownSRC) {preLoadImg(img.mdownSRC); img.onmousedown = imgMouseDownSwap;}
                if (img.mupSRC) {preLoadImg(img.mupSRC); img.onmouseup = imgMouseUpSwap;}
                img.rolloverSet = true;
            }
        }
        function preLoadImg(imgSrc) {
            prel[prel.length] = new Image(); prel[prel.length-1].src = imgSrc;
        }
    }

    function imgHoverSwap() {this.src = this.hoverSRC;}
    function imgOutSwap() {this.src = this.outSRC;}
    function imgMouseDownSwap() {this.src = this.mdownSRC;}
    function imgMouseUpSwap() {this.src = this.mupSRC;}

    function setImageSwaps() {
        prepareImageSwap(document.body);
    }


    window.addEvent('domready', function() {
        var slideSearch = new Fx.Slide('slidebox_search');
        var slideLogin = new Fx.Slide('slidebox_login');
        var slideForum = new Fx.Slide('slidebox_forum');
        var slidePolls = new Fx.Slide('slidebox_polls');

        $('slide_search').addEvent('click', function(e){
            e.stop();
            slideSearch.toggle();
        });

        $('slide_login').addEvent('click', function(e){
            e.stop();
            slideLogin.toggle();
        });

        $('slide_forum').addEvent('click', function(e){
            e.stop();
            slideForum.toggle();
        });

        $('slide_polls').addEvent('click', function(e){
            e.stop();
            slidePolls.toggle();
        });

    });