function FileImageProcessor(inputFile, divImages) {
    if (!blobFiles.find(element => element.origen === inputFile)) {
        blobFiles.push(new BlobFile(new Array(), inputFile));
    }

    document.getElementById(inputFile).onchange = function () {
        var blobs = blobFiles.find(element => element.origen === inputFile).blobFile;
        blobs.push(this.files[0]);
        var name = this.files[0].name;
        var reader = new FileReader();

        reader.readAsDataURL(this.files[0]);
        reader.onload = function (evt) {
            if (evt.target.readyState === FileReader.DONE) {
                $(divImages)
                    .append('<div class="col-md-3" style="background-color:lightgray "><img class="imgdinamico" style="border-radius:10px" width="100%" id="' + name + '" src=' + evt.target.result + ' ></img></div>');
                download(evt.target.result, name, "image/*");
            }
        }
    };
}