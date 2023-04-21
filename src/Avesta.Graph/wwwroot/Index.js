//#region [- json viewr -]

var jsonObj = {};
var jsonViewer = new JSONViewer();
document.querySelector("#json").appendChild(jsonViewer.getContainer());

// textarea value to JSON object
var setJSON = function (data) {
    try {
        var value = JSON.stringify(data)
        jsonObj = JSON.parse(value);
    } catch (err) {
        alert(err);
    }
};

// load default value
setJSON("{}");
jsonViewer.showJSON(jsonObj);

//#endregion

//#region [- splitter -]

// set EVERY 'state' here so will undo ALL layout changes
// used by the 'Reset State' button: myLayout.loadState( stateResetSettings )
var stateResetSettings = {
    north__size: "auto",
    north__initClosed: false,
    north__initHidden: false,
    south__size: "auto",
    south__initClosed: false,
    south__initHidden: false,
    east__size: 600,
    east__initClosed: false,
    east__initHidden: false
};

var myLayout;

$(document).ready(function () {

    // this layout could be created with NO OPTIONS - but showing some here just as a sample...
    // myLayout = $('body').layout(); -- syntax with No Options

    myLayout = $('body').layout({

        //	reference only - these options are NOT required because 'true' is the default
        closable: true // pane can open & close
            ,
        resizable: true // when open, pane can be resized 
            ,
        slidable: true // when closed, pane can 'slide' open over other panes - closes on mouse-out
            ,
        livePaneResizing: true

            //	some resizing/toggling settings
            ,
        south__slidable: false // OVERRIDE the pane-default of 'slidable=true'
            ,
        south__togglerLength_closed: '100%' // toggle-button is full-width of resizer-bar
            ,
        south__spacing_closed: 20 // big resizer-bar when open (zero height)

            //	some pane-size settings
            ,
        west__minSize: 100,
        east__size: .5,
        east__minSize: 200,
        east__maxSize: .9 // 50% of layout width
            ,
        center__minWidth: 100

            //	some pane animation settings
            ,
        west__animatePaneSizing: false,
        west__fxSpeed_size: "fast" // 'fast' animation when resizing west-pane
            ,
        west__fxSpeed_open: 1000 // 1-second animation when opening west-pane
            ,
        west__fxSettings_open: {
            easing: "easeOutBounce"
        } // 'bounce' effect when opening
        ,
        west__fxName_close: "none" // NO animation when closing west-pane

            //	enable showOverflow on west-pane so CSS popups will overlap north pane
            ,
        west__showOverflowOnHover: true

            //	enable state management
            ,
        stateManagement__enabled: true // automatic cookie load & save enabled by default

            ,
        showDebugMessages: true // log and/or display messages from debugging & testing code
    });




});



//#endregion

//#region [- hierarchy -]

let hide = false

function toggleTypes() {
    var els = document.getElementsByClassName("type");
    Array.from(els).forEach(function (el) {
        if (!hide) {
            el.style.display = 'none'

        } else {
            el.style.display = ''
        }
    });
    hide = !hide;
}




function toggle() {
    var toggler = document.getElementsByClassName("caret");
    var i;

    for (i = 0; i < toggler.length; i++) {
        toggler[i].addEventListener("click", function () {
            this.parentElement.querySelector(".nested").classList.toggle("active");
            this.classList.toggle("caret-down");
        });
    }
}



function initProperty(property) {
    var id = uuidv4();
    var result =
        `<li><a id='${id}' onclick='showQueryManagerModal("${id}")' href='#'>${property.Name}</a> <span class='prop-type type' href='#'>: ${property.TypeShowName}</span></li>`;

    initNewQuery(property, id)
    return result;
}

function initEntity(entity) {

    let props = '';

    entity.Properties.forEach(p => {
        props += initProperty(p)
    })

    var data = `
<ul class="myUL" id='${uuidv4()}'>
  <li><span class="caret">${entity.Name}</span>
    <ul class="nested">
      ${props}
    </ul>
  </li>
</ul>
`

    return data;
}

function initHierarchy(hierarchy) {
    let result = '';

    (hierarchy.Entities).forEach(entity => {
        result += initEntity(entity)
    });

    return result;
}



getConfig()
//#endregion

//#region [- http requests -]


function getConfig() {
    $.ajax('https://localhost:7194/avesta/graph/graph.json').done((response) => {
        var hierarchy = new DataHierarchy(JSON.parse(response));
        var result = initHierarchy(hierarchy);
        $("#hierarchy-container").html(result)
        toggle();
    })
}

function sendQuery() {
    $.get('https://localhost:7194/avesta/graph/api/global/exe', {
        where: REQUESTCONFIG['query'],
        typeFullName : REQUESTCONFIG['type']
    }, function (response) {
        console.log(response)
        jsonViewer.showJSON(response);

    });
}

//#endregion

//#region [- query monitoring -]
let propertiesQuery = []
let getQueryById = (id) => {
    var pQuery = propertiesQuery.filter((q) => {
        if (q.Id == id)
            return q;
    })
    return pQuery[0];
}

let getPropertyQueryAsJson = () => {
    return JSON.stringify(propertiesQuery)
}
let initNewQuery = (propertyInformation, id) => {
    var pq = new PropertyQuery({
        PropertyInformation: propertyInformation,
        Id: id,
        QueryHTML: ""
    })
    propertiesQuery.push(pq)
}
let updateQuery = (query, id) => {
    q = getQueryById(id);
    q.Query = query;
}

function showQueryManagerModal(id) {
    $(".bd-example-modal-lg").modal("show")
    monitoringQueryInfo(id)
}

function monitoringQueryInfo(id) {
    var pQuery = getQueryById(id)
    var result = `${pQuery.QueryHTML} ${getQueryChooseSelectBox(pQuery.PropertyInformation.Name)}`
    $('#q-monitor-container').html(result)
    $('#q-monitor-container').attr("current-query-id", pQuery.Id)
}


function getQueryChooseSelectBox(name) {
    var select = `
 <div class="dropdown mt-5">
 <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
   Operations
 </button>
 <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
   <a href="#" class="m-1" onclick = 'generateEqualQueryHTML("${name}")'>Equal</a>
   <br/><a href="#" class="m-1" onclick = 'generateContainsQueryHTML("${name}")'>Contains</a>
   <br/><a href="#" class="m-1" onclick = 'generateGreaterThanQueryHTML("${name}")'>Greater Than</a>
   <br/><a href="#" class="m-1" onclick = 'generateGreaterThanOrEqualQueryHTML("${name}")'>Greater Than Or Equal</a>
   <br/><a href="#" class="m-1" onclick = 'generateLowerThanQueryHTML("${name}")'>Lower Than Or Equal</a>
   <br/><a href="#" class="m-1" onclick = 'generateLowerThanOrEqualQueryHTML("${name}")'>Lower Than Or Equal</a>
 </div>
</div>
    
    `
    return select;
}


function toggleOperator(elm) {
    var value = $(elm).text()
    if (value == "OR") {
        $(elm).text("AND")
        $(elm).attr("linq-query", "&&")
    }
    if (value == "AND") {
        $(elm).text("OR")
        $(elm).attr("linq-query", "||")
    }

    initializeQueryForSpecificPropertyById()
}

function refreshMonitor(newQuery, operatorId) {
    var id = $('#q-monitor-container').attr("current-query-id")
    var q = `${newQuery} <a id='operate-${operatorId}' href='#' style='margin:20px' linq-query='||' onclick='toggleOperator(this)'>OR</a> `
    var pQuery = getQueryById(id)
    pQuery.QueryHTML += q;
    var result = `${pQuery.QueryHTML} ${getQueryChooseSelectBox(pQuery.PropertyInformation.Name)}`
    $('#q-monitor-container').html(result)
}


function changeLinqValue(query, id) {
    var value = $(`#input-${id}`).val()
    $(`#span-${id}`).attr("linq-value", query.replace("$TARGET", value))
    initializeQueryForSpecificPropertyById()
}


function initializeQueryForSpecificPropertyById() {
    var all_query = ''
    var spans = $("[linq-value]")
    Array.from(spans).forEach(s => {
        var query = $(s).attr("linq-value")

        var id = $(s).attr("id").replace("span-", "")

        var operand = $(`#operate-${id}`).attr("linq-query")
        all_query += ` ${query} ${operand}`
    })

    var id = $('#q-monitor-container').attr("current-query-id")
    var pQuery = getQueryById(id)
    all_query = cleanQuery(all_query)
    pQuery.Query = all_query;

    $("#q-translator-container").html(colorize(all_query))
    combineAllQueries()
}


let REQUESTCONFIG = {
    query: '',
    type: 'Avesta.Graph.Test.Src.Data.Model.School'
}


function combineAllQueries() {
    var result = ''
    propertiesQuery.forEach(pq => {
        if (pq.Query == undefined || pq.Query == null || pq.Query == '')
            return;

        result += `(${pq.Query}) ||`
    })

    result = cleanQuery(result)
    REQUESTCONFIG['query'] = `i => ${result}`;

    $('#query-container').html(colorize(`i => ${result}`))
}





let generateEqualQueryHTML = (name) => {
    var id = uuidv4();
    var body = `<span id='span-${id}' linq-value='' class="mt-3">${name} = <input id='input-${id}' onkeyup="changeLinqValue('i.${name} == $TARGET', '${id}')" /></span>`
    refreshMonitor(body, id);
}
let generateContainsQueryHTML = (name) => {
    var id = uuidv4();
    var body = `<span id='span-${id}' linq-value='' class="mt-3">${name}.contains(<input id='input-${id}' onkeyup="changeLinqValue('i.${name}.Contains($TARGET) ', '${id}')" />)</span>`
    refreshMonitor(body, id);
}
let generateGreaterThanQueryHTML = (name) => {
    var id = uuidv4();
    var body = `<span id='span-${id}' linq-value='' class="mt-3">${name} > <input id='input-${id}' onkeyup="changeLinqValue('i.${name} > $TARGET', '${id}')" /></span>`
    refreshMonitor(body, id);
}
let generateLowerThanQueryHTML = (name) => {
    var id = uuidv4();
    var body = `<span id='span-${id}' linq-value='' class="mt-3">${name} < <input id='input-${id}' onkeyup="changeLinqValue('i.${name} < $TARGET ', '${id}')" /></span>`
    refreshMonitor(body, id);
}
let generateGreaterThanOrEqualQueryHTML = (name) => {
    var id = uuidv4();
    var body = `<span id='span-${id}' linq-value='' class="mt-3">${name} >= <input id='input-${id}' onkeyup="changeLinqValue('i.${name} >= $TARGET', '${id}')" /></span>`
    refreshMonitor(body, id);
}
let generateLowerThanOrEqualQueryHTML = (name) => {
    var id = uuidv4();
    var body = `<span id='span-${id}' linq-value='' class="mt-3">${name} <= <input id='input-${id}' onkeyup="changeLinqValue('i.${name} <= $TARGET ', '${id}')" /></span>`
    refreshMonitor(body, id);
}


//#endregion

//#region [- models -]


class BaseModel {
    CreatedDate;
    ID;
    ModifiedDate;
    DeletedDate;
    IsLock;

    constructor(obj) {
        obj && Object.assign(this, obj)
    }
}

class AvestaGraphModel extends BaseModel {
    constructor(obj) {
        super(obj)
        obj && Object.assign(this, obj)
    }
}

class BasicInformation extends AvestaGraphModel {
    Name;
    FullName;
    TypeFullName;
    TypeShowName;

    constructor(obj) {
        super(obj)
        obj && Object.assign(this, obj)
    }

}

class EntityInformation extends BasicInformation {
    Properties;
    constructor(obj) {
        super(obj)

        let lstemp = []
        for (let i = 0; i < obj['Properties'].length; i++) {
            var model = new PropertyInformation(obj.Properties[i])
            lstemp[i] = model
        }
        this.Properties = lstemp;

        obj && Object.assign(this, obj)
    }
}

class PropertyInformation extends BasicInformation {
    constructor(obj) {
        super(obj)
        obj && Object.assign(this, obj)
    }
}

class DataHierarchy extends BasicInformation {
    Entities;
    constructor(obj) {
        super(obj)

        let lstemp = []
        for (let i = 0; i < obj['Entities'].length; i++) {
            var model = new EntityInformation(obj.Entities[i])
            lstemp[i] = model
        }
        this.Entities = lstemp;


        obj && Object.assign(this, obj)
    }
}




class PropertyQuery {
    Query;
    QueryHTML;
    PropertyInformation;
    Id;

    constructor(obj) {
        obj && Object.assign(this, obj)
    }

    setId(id) {
        this.Id = id;
    }
}


//#endregion

//#region [- utilities -]


function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

function colorize(data) {
    data = data.replaceAll("==", "<span style='color:blue'>==</span>")
    data = data.replaceAll("\"", "<span style='color:red'>\"</span>")
    data = data.replaceAll("&&", "<span style='color:green'>&&</span>")
    data = data.replaceAll("||", "<span style='color:pink'>||</span>")
    return data;
}

function cleanQuery(query) {
    return query.slice(0, -2)
}

function removeLastInstance(badtext, str) {
    var charpos = str.lastIndexOf(badtext);
    if (charpos < 0) return str;
    ptone = str.substring(0, charpos);
    pttwo = str.substring(charpos + (badtext.length));
    return (ptone + pttwo);
}
//#endregion