 $(document).ready(function () {
        $("#HinhAnhFiles").change(function () {
            $("#previewImages").html("");
            var input = this;
            if (input.files && input.files[0]) {
                for (var i = 0; i < input.files.length; i++) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $("#previewImages").append('<img src="' + e.target.result + '" width="150" height="150" />');
                    };
                    reader.readAsDataURL(input.files[i]);
                }
            }
        });
 });
