$(function () { let o = $(" #Products .pro-left .cat-down-item span"), t = $(" #Products .pro-left ul"); o.on("click", function () { t.stop().slideToggle(500) }) });