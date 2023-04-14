var data = {
    "items": {
        "item": [{
            "id": "0001",
            "type": "donut",
            "name": "Cake",
            "ppu": 0.55,
            "batters": {
                "batter": [{
                        "id": "1001",
                        "type": "Regular"
                    },
                    {
                        "id": "1002",
                        "type": "Chocolate"
                    },
                    {
                        "id": "1003",
                        "type": "Blueberry"
                    },
                    {
                        "id": "1004",
                        "type": "Devil's Food"
                    }
                ]
            },
            "topping": [{
                    "id": "5001",
                    "type": "None"
                },
                {
                    "id": "5002",
                    "type": "Glazed"
                },
                {
                    "id": "5005",
                    "type": "Sugar"
                },
                {
                    "id": "5007",
                    "type": "Powdered Sugar"
                },
                {
                    "id": "5006",
                    "type": "Chocolate with Sprinkles"
                },
                {
                    "id": "5003",
                    "type": "Chocolate"
                },
                {
                    "id": "5004",
                    "type": "Maple"
                }
            ]
        }]
    }
};

var jsonObj = {};
var jsonViewer = new JSONViewer();
document.querySelector("#json").appendChild(jsonViewer.getContainer());

// textarea value to JSON object
var setJSON = function () {
    try {
        var value = JSON.stringify(data)
        jsonObj = JSON.parse(value);
    } catch (err) {
        alert(err);
    }
};

// load default value
setJSON();
jsonViewer.showJSON(jsonObj);





















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

function showQueryManagerModal(id) {
    $(".bd-example-modal-lg").modal("show")
    monitoringQueryInfo(id)
}

function monitoringQueryInfo(id) {
    var pQuery = getQueryById(id)
    console.log(pQuery)
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
    if (value == "OR")
        $(elm).text("AND")
    if (value == "AND")
        $(elm).text("OR")
}

function refreshMonitor(newQuery) {
    var id = $('#q-monitor-container').attr("current-query-id")
    var q = `${newQuery} <a href='#' style='margin:20px' onclick='toggleOperator(this)'>OR</a> `
    var pQuery = getQueryById(id)
    pQuery.QueryHTML += q;
    var result = `${pQuery.QueryHTML} ${getQueryChooseSelectBox(pQuery.PropertyInformation.Name)}`
    $('#q-monitor-container').html(result)
}


function changeLinqValue(query, id) {
    var value = $(`#input-${id}`).val()
    $(`#span-${id}`).attr("linq-value", `${query} ${value}`)
}


let generateEqualQueryHTML = (name) => {
    var id = uuidv4();
    var body = `<span id='span-${id}' linq-value='' class="mt-3">${name} = <input id='input-${id}' onkeyup="changeLinqValue('i.${name} = ', '${id}')" /></span>`
    refreshMonitor(body);
}
let generateContainsQueryHTML = (name) => {
    var body = `<span class="mt-3">${name}.contains(<input />)</span>`
    refreshMonitor(body);
}
let generateGreaterThanQueryHTML = (name) => {
    var body = `<span class="mt-3">${name} > <input /></span>`
    refreshMonitor(body);
}
let generateLowerThanQueryHTML = (name) => {
    var body = `<span class="mt-3">${name} < <input /></span>`
    refreshMonitor(body);
}
let generateGreaterThanOrEqualQueryHTML = (name) => {
    var body = `<span class="mt-3">${name} >= <input /></span>`
    refreshMonitor(body);
}
let generateLowerThanOrEqualQueryHTML = (name) => {
    var body = `<span class="mt-3">${name} <= <input /></span>`
    refreshMonitor(body);
}


function translateHTMLQueryToLinq(queryHTML) {

}


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



function getConfig() {
    $.ajax('http://localhost:7194/avesta/graph/graph.json').done((response) => {
        var hierarchy = new DataHierarchy(JSON.parse(response));
        console.log(hierarchy)
        var result = initHierarchy(hierarchy);
        $("#hierarchy-container").html(result)
        toggle();
    })
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


function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

getConfig()