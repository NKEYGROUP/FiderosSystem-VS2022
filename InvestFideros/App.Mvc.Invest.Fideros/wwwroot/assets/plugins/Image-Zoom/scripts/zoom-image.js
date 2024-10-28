/**
 * by MonsterDuang
 */
;(function($) {
    /**
     * 1, The size of the thumbnail is the same as the size of the parent container
     * 2, The href attribute of the parent container is the path of the high-definition image
     */
    $.fn.zoomImage = function(paras) {
        /**
         * Default parameters
         */
        var defaultParas = {
            layerW: 100, // Mask width
            layerH: 100, // Mask height
            layerOpacity: 0.2, // Mask transparency
            layerBgc: '#000', // Mask background color
            showPanelW: 500, // Show the magnified area width
            showPanelH: 500, // Display the height of the magnified area
            marginL: 5, // The distance between the zoomed area and the right side of the thumbnail
            marginT: 0 // The distance between the enlarged area and the upper side of the thumbnail
        };

        paras = $.extend({}, defaultParas, paras);

        $(this).each(function() {
            var self = $(this).css({position: 'relative'});
            var selfOffset = self.offset();
            var imageW = self.width(); // Picture height
            var imageH = self.height();

            self.find('img').css({
                width: '100%',
                height: '100%'
            });

            // wide magnification
            var wTimes = paras.showPanelW / paras.layerW;
            // High magnification
            var hTimes = paras.showPanelH / paras.layerH;

            
            // Zoom in picture
            var img = $('<img>').attr('src', self.attr("href")).css({
                position: 'absolute',
                left: '0',
                top: '0',
                width: imageW * wTimes,
                height: imageH * hTimes
            }).attr('id', 'big-img');
            // Mask
            var layer = $('<div>').css({
                display: 'none',
                position: 'absolute',
                left: '0',
                top: '0',
                backgroundColor: paras.layerBgc,
                width: paras.layerW,
                height: paras.layerH,
                opacity: paras.layerOpacity,
                border: '1px solid #000',
                cursor: 'crosshair'
            });
console.log(selfOffset)
            // zoom in area
            var showPanel = $('<div>').css({
                display: 'none',
                position: 'absolute',
                overflow: 'hidden',
                left: imageW + paras.marginL,
                top: paras.marginT,
                width: paras.showPanelW,
                height: paras.showPanelH
            }).append(img);

            self.append(layer).append(showPanel);

            self.on('mousemove', function(e) {
                // The coordinates of the mouse relative to the thumbnail container
                var x = e.pageX - selfOffset.left;
                var y = e.pageY - selfOffset.top;

                if(x <= paras.layerW / 2) {
                    x = 0;
                }else if(x >= imageW - paras.layerW / 2) {
                    x = imageW - paras.layerW;
                }else {
                    x = x - paras.layerW / 2;
                }

                if(y < paras.layerH / 2) {
                    y = 0;
                } else if(y >= imageH - paras.layerH / 2) {
                    y = imageH - paras.layerH;
                } else {
                    y = y - paras.layerH / 2;
                }

                layer.css({
                    left: x,
                    top: y
                });

                img.css({
                    left: -x * wTimes,
                    top: -y * hTimes
                });
            }).on('mouseenter', function() {
                layer.show();
                showPanel.show();
            }).on('mouseleave', function() {
                layer.hide();
                showPanel.hide();
            });
        });
    }
})(jQuery);
