$(function() {
    let currentSlide = 0;
    $("#Slider").css("background", $(".slide-img").eq(currentSlide).css("background"))

    $(".btnMain").click(function() {

        SliderShow()
    })
    setInterval(() => {
        SliderShow()
    }, 6000);

    function SliderShow() {
        $(".slider-item").eq(currentSlide).removeClass().addClass("slider-item")
        $(".slider-item").eq(currentSlide).css("top", "-100%")
        currentSlide = (currentSlide + 1 + $(".slider-item").length) % $(".slider-item").length
        $(".slider-item").eq(currentSlide).addClass("slide-active")
        $(".slider-item").eq(currentSlide).css("top", "0%")
        $(".dots-list .dot-item").removeClass("dot-active")
        $(".dots-list .dot-item").eq(currentSlide).addClass("dot-active")
        $("#Slider").css("background", $(".slide-img").eq(currentSlide).css("background"))

        // $("#Slider").css("background", $(".slider-item").eq(currentSlide).css("backgroundImage"))
    }
    const dotDiv = $("<div class='dots-list'></div>")
    $(".slider-item").each(function(ind, elm) {
        let dotitem = $("<div sld='" + ind + "' class='dot-item'></div>")
        dotDiv.append(dotitem)
    })
    $("#Slider").append(dotDiv)

    $(".dots-list .dot-item").click(function() {
        $(".dots-list .dot-item").removeClass("dot-active")
        $(this).addClass("dot-active")
        let index = $(this).attr("sld")
        $(".slider-item").eq(currentSlide).removeClass("slide-active")
        $(".slider-item").eq(currentSlide).css("top", "-100%")
        currentSlide = index;
        $(".slider-item").eq(currentSlide).addClass("slide-active")
        $(".slider-item").eq(currentSlide).css("top", "0%")
        $("#Slider").css("background", $(".slide-img").eq(currentSlide).css("background"))

    })
})