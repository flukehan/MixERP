//Gumbo (StackOverflow)
//http://stackoverflow.com/questions/6238351/fastest-way-to-detect-external-urls
function isExternal(url) {
    var match = url.match(/^([^:\/?#]+:)?(?:\/\/([^\/?#]*))?([^?#]+)?(\?[^#]*)?(#.*)?/);
    if (typeof match[1] === "string" && match[1].length > 0 && match[1].toLowerCase() !== location.protocol) return true;
    if (typeof match[2] === "string" && match[2].length > 0 && match[2].replace(new RegExp(":("+{"http:":80,"https:":443}[location.protocol]+")?$"), "") !== location.host) return true;
    return false;
};

function convertDocument(text)
{
    return $(marked(text));
};

function processDocument(url)
{
    var xhr= new XMLHttpRequest();
    xhr.open('GET', url, true);
    xhr.onreadystatechange= function() {
        if (this.readyState!==4) return;
        if (this.status!==200) return;
        
        var content = $("#content");
        var html = convertDocument(this.responseText);
        html = processImages(html);

        content.html(html);
        var header = content.find("h1, h2, h3").html();

        if(header)
        {
            document.title = header;            
        }

        $(".footer").show();

        createSubTopics();
        processAnchors();
        processVideos();
    };
    xhr.send();
};

function processImages(html) {
    var images = $(html).find("img");
    var path = getPath();

    images.each(function () {
        var el = $(this);
        var src = path + el.attr("src");
        $(this).attr("src", src);
    });

    return html;
};

function processVideos() {
    var videos = $("#content").find("video");
    var path = getPath();


    videos.each(function () {
        var el = $(this);
        var src = path + el.attr("src");
        $(this).attr("src", src);
    });

    return html;
};

function getPath()
{
    var path = window.location.hash.replace("#", "");
    path = path.substring(0,path.lastIndexOf("/")+1);
    return path;    
}

function processAnchors()
{
    var anchors = $("#content").find("a");
    var path = getPath();
    
    anchors.each(function(){
        var el = $(this);
        var href = el.attr("href");
        
        if(href)
        {
            if(isExternal(href))
            {
                el.attr("target", "_blank");                
            }
            else
            {
                href = path + href;
                href = URI(href).normalizePathname()._parts.path
                el.attr("href", "#" + href);                
            }
        }
    });
    
    anchors.click(function(){
        var href = $(this).attr("href");
        if(!isExternal(href))
        {
            window.location = href;
            window.location.reload();
        }
    });
};


window.onload = function()
{
    loadDocument();
    $(document).foundation();
    $(document).foundation('topbar', 'reflow');
    $("#content").css("min-height", $(window).height() + "px");
};

function createSubTopics() {
$("#content").find("h1, h2, h3").each(function () {
    var topics = $(".topics");
    var $section = $(this);
    var safeName = $section.attr("id");
    var id;
    var text = $section.text();

    if (!safeName) {
        safeName = text.trim().replace(/\s+/g, '-').replace(/[^-,'A-Za-z0-9]+/g, '').toLowerCase();
        id = window.escape(safeName);
        $section.attr("id", id);
    };

    id = window.escape(safeName);
    var c = topics.find('a[href="#' + id + '"]');

    var anchor = "<li><a class='item' href='#" + id + "'>" + text + "</a></li>";
    topics.append(anchor);            
});
};

function loadDocument()
{
    var url = window.location.hash.replace("#", "");
    if(!url)
    {
        url = "index.md";
        window.location.hash = url;
    }
    processDocument(url);    
};

window.onhashchange = function (event) {
    if(window.location.hash)
    {
        loadDocument();
        top.scroll(0, 0);
    };
};        
