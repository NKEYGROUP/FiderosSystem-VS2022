(function () {
	"use strict";

	var slideMenu = $('.side-menu');

	// Toggle Sidebar
		$('[data-bs-toggle="sidebar"]').click(function(event) {
			event.preventDefault();
			$('.app').toggleClass('sidenav-toggled');
		});

		$(window).on('load resize',function(){
			if($(window).width() < 739){
				$('.app-sidebar').hover(function(event) {
				event.preventDefault();
				$('.app').addClass('sidenav-toggled');
				});
			}
				if($(window).width() > 739){
							$('.app-sidebar').hover(function(event) {
				event.preventDefault();
				$('.app').removeClass('sidenav-toggled');
				});
				}
		});

		// Activate sidebar slide toggle
		$("[data-bs-toggle='slide']").click(function(event) {
			event.preventDefault();
			if(!$(this).parent().hasClass('is-expanded')) {
				slideMenu.find("[data-bs-toggle='slide']").parent().removeClass('is-expanded');
			}
			$(this).parent().toggleClass('is-expanded');
		});

		$("[data-bs-toggle='sub-slide']").click(function(event) {
			event.preventDefault();
			if(!$(this).parent().hasClass('is-expanded')) {
				slideMenu.find("[data-bs-toggle='sub-slide']").parent().removeClass('is-expanded');
			}
			$(this).parent().toggleClass('is-expanded');
		});

		// // Set initial active toggle
		$("[data-bs-toggle='slide.'].is-expanded").parent().toggleClass('is-expanded');

		// //Activate bootstrip tooltips
		var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
		var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
		return new bootstrap.Tooltip(tooltipTriggerEl)
		})
			// Set initial active toggle
	$(".sub-slide-show.is-expanded").parent().toggleClass('is-expanded');
	$(".child-sub-menu.open").parent().parent().parent().parent().parent().toggleClass('is-expanded');

	
	
	// Activate sidebar slide toggle
	$(".slide-show").on('click', function(e) {
		var $this = $(this);
		var checkElement = $this.next();
		var animationSpeed = 300,
		slideMenuSelector = '.slide-menu';
		if (checkElement.is(slideMenuSelector) && checkElement.is(':visible')) {
		  checkElement.slideUp(animationSpeed, function() {
			checkElement.removeClass('open');
		  });
		  checkElement.parent("li").removeClass("is-expanded");
		}
		 else if ((checkElement.is(slideMenuSelector)) && (!checkElement.is(':visible'))) {
		  var parent = $this.parents('ul').first();
		  var ul = parent.find('ul:visible').slideUp(animationSpeed);
		  ul.removeClass('open');
		  var parent_li = $this.parent("li");
		  checkElement.slideDown(animationSpeed, function() {
			checkElement.addClass('open');
			parent.find('li.is-expanded').removeClass('is-expanded');
			parent_li.addClass('is-expanded');
		  });
		}
		if (checkElement.is(slideMenuSelector)) {
		  e.preventDefault();
		}
	});
	
	// Activate sidebar slide toggle
	$(".sub-slide-show").on('click', function(e) {
		var $this = $(this);
		var checkElement = $this.next();
		var animationSpeed = 300,
		slideMenuSelector = '.child-sub-menu';
		if (checkElement.is(slideMenuSelector) && checkElement.is(':visible')) {
		  checkElement.slideUp(animationSpeed, function() {
			checkElement.removeClass('open');
		  });
		  checkElement.parent("li").removeClass("is-expanded");
		}
		 else if ((checkElement.is(slideMenuSelector)) && (!checkElement.is(':visible'))) {
		  var parent = $this.parents('ul').first();
		  var ul = parent.find('ul:visible').slideUp(animationSpeed);
		  ul.removeClass('open');
		  var parent_li = $this.parent("li");
		  checkElement.slideDown(animationSpeed, function() {
			checkElement.addClass('open');
			parent.find('li.is-expanded').removeClass('is-expanded');
			parent_li.addClass('is-expanded');
		  });
		}
		if (checkElement.is(slideMenuSelector)) {
		  e.preventDefault();
		}
	});

		// Activate sidebar slide toggle
	$(".sub-slide2-show").on('click', function(e) {
		var $this = $(this);
		var checkElement = $this.next();
		var animationSpeed = 300,
		slideMenuSelector = '.child-sub-menu2';
		if (checkElement.is(slideMenuSelector) && checkElement.is(':visible')) {
		  checkElement.slideUp(animationSpeed, function() {
			checkElement.removeClass('open');
		  });
		  checkElement.parent("li").removeClass("is-expanded");
		}
		 else if ((checkElement.is(slideMenuSelector)) && (!checkElement.is(':visible'))) {
		  var parent = $this.parents('ul').first();
		  var ul = parent.find('ul:visible').slideUp(animationSpeed);
		  ul.removeClass('open');
		  var parent_li = $this.parent("li");
		  checkElement.slideDown(animationSpeed, function() {
			checkElement.addClass('open');
			parent.find('li.is-expanded').removeClass('is-expanded');
			parent_li.addClass('is-expanded');
		  });
		}
		if (checkElement.is(slideMenuSelector)) {
		  e.preventDefault();
		}
	});
	
	// ______________Active Class
	$(document).ready(function() {
		$(".app-sidebar a").each(function() {
		  var pageUrl = window.location.href.split(/[?#]/)[0];
			if (this.href == pageUrl) { 
				$(this).addClass("active");
				$(this).parent().addClass("is-expanded");
				$(this).parent().parent().prev().addClass("active"); 
				$(this).parent().parent().addClass("open"); 
				$(this).parent().parent().prev().addClass("is-expanded"); 
				$(this).parent().parent().parent().addClass("is-expanded"); 
				$(this).parent().parent().parent().parent().addClass("open"); 
				$(this).parent().parent().parent().parent().prev().addClass("active"); 
				$(this).parent().parent().parent().parent().parent().addClass("is-expanded"); 
			}
		});
	});

})();