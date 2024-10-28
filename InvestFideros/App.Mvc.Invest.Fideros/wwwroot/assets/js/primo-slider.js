$(function () {
	"use strict";
	/***************** RTL *********************/
	$('#myonoffswitch2').click(function () {
		if (this.checked) {
			_bs('.carousel').create.trigger('rtl');
		}
	});
	/***************** RTL *********************/

	/***************** LTR *********************/

	$('#myonoffswitch1').click(function () {
		if (this.checked) {
			_bs('.carousel').create.trigger('ltr');
		}
	});
	/***************** LTR *********************/


	/***************** RTL HAs Class *********************/

	let bodyRtl = $('body').hasClass('rtl');
	if (bodyRtl) {
		_bs('.carousel').create.trigger('rtl');
	}
	/***************** RTL HAs Class *********************/
})