$(function () {
    let t = [];
    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }
    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
    function a() {
        $(".bloader", "#cartModal").show(),
            $(".cartItems", "#cartModal").html(""),
            $.ajax({
                url: "/Orders/CartProducts",
                contentType: "application/json; charset=utf-8",
                dataType: "html"
            })
            .done(function (a) {
                    $(".bloader", "#cartModal").show(),
                        $(".cartItems", "#cartModal").html(a),
                        $(".prd-remove").click(function () {
                            var a = $(this).attr("pro-id");
                            (t = t.filter((t) => t.productId !== a)),
                                setCookie("cartItems", JSON.stringify(t),30),
                                $(this).parents("tr").remove(),
                                $(".btn-checkout a[pro-id='" + a + "']")
                                    .html('\n                                <img style="width:8%;color:#fff;" src="/FrontPublic/Uploads/shopping-cart.svg" />\n                                <span class="ml-2">Səbətə at</span>')
                                    .removeClass("disabled");
                        });
                })
                .fail(function () {
                    swal.fire("error", "Sifariş zamanı xəta baş verdi.", "warning");
                });
    }
    !(function () {
        var s = Cookies.get("cartItems");
        if (void 0 !== s && "" !== s) {
            t = JSON.parse(s);
            var i = [...$(".btn-checkout a").map((t, a) => a)];
            (i = i.filter((a) => t.map((t) => t.productId).includes($(a).attr("pro-id")))),
                $(i).html('\n <i class="fas fa-check text-green"></i>\n <span class="ml-2"> Əlavə edildi</span>'),
                $(i).addClass("disabled"),
                a();
        }
    })(),
        $(".btn-clear-pro").click(function () {
            (t = []),
                Cookies.set("cartItems", t),
                $(".cartItems", "#cartModal").html('\n<div class="alert alert-warning mb-0" role="alert">\n                Səbətdə məhsul yoxdur.\n            </div>'),
                $(".btn-checkout a").html(
                    '\n<img style="width:8%;color:#fff;" src="/FrontPublic/Uploads/shopping-cart.svg" />\n            <span class="ml-2">Səbətə at</span>').removeClass("disabled");
        }),
        $(".btn-checkout a").click(function (s) {
            var i = $(this).attr("pro-id"),
                r = $(this).attr("pro-quantity");
            if (($(this).find("img").hide(), $(this).html('\n  <i class="fas fa-check text-green"></i>\n           <span class="ml-2"> Əlavə edildi</span>'), null != i && "" != i)) {
                const e = { productId: i, quantity:r };
                t.push(e),
                setCookie("cartItems", JSON.stringify(t), 30),
                    $(this).addClass("disabled"), a(), s.preventDefault();
            }
        }),
        $("[type='number'].changeQuantity").change(function (a) {
            let s = $(this).val(),
                i = $(this).attr("min");
            s <= i && ($(this).val(""), $(this).val(i)),
                (t = []),
                $.each($("[type='number'].changeQuantity"), function (a, s) {
                    var i = $(this).attr("data-id"),
                        r = $(this).val();
                    const e = { productId: i, quantity: r };
                    t.push(e);
                }),
                setCookie("cartItems", JSON.stringify(t), 30);
        });
});
