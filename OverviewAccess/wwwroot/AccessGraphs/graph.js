$(document).ready(function () {

    $.ajax({
        url: "https://localhost:44387/CentralAccessManagement/api/v1/CRUDAccess/GetAllAccess",
        type: "get",
      //  dataType: 'jsonp',
        headers: { 'Access-Control-Allow-Origin': 'true' },
      // crossDomain: true,
        success: function (data) {
            generateGraph(data)
        },
        error: function (xhr, status, error){
            console.log(error);
            console.log(status);
            console.log(xhr);

            alert(xhr.responseText);
        }
    })

});



function updateGraphHeight() {
    const height = $(window).height() - 200;
    $('#graph-container').height(height);
}

$(window).on('resize', updateGraphHeight);
updateGraphHeight();


function generateGraph(data) {
    console.log(data)


    let nodes = [];

    for (item of data) {
        let fromNode = { data: { id: item.from.address, label: item.from.address } };
        let toNode = { data: { id: item.to.address, label: item.to.address } };

        nodes.push(fromNode);

        nodes.push(toNode);
    }

    let edges = [];

    for (item of data) {
        let edge = { data: { source: item.from.address, target: item.to.address, label: item.port }, classes: 'autorotate' };
        edges.push(edge);
    }

    var cy = window.cy = cytoscape({
        container: document.getElementById('cy'),

        layout: {
            name: 'concentric',
            concentric: function (n) { return n.id() === 'j' ? 10 : 0; },
            levelWidth: function (nodes) { return 100; },
            minNodeSpacing: 100,
        },
        
        
        
        style: [
            {
                "selector": "node[label]",
                "style": {
                    "label": "data(label)",
                    "background-color": "orange",
                    "text": "bold"
                }
            },

            {
                "selector": "edge[label]",
                "style": {
                    "label": "data(label)",
                    "width": 3,
                    "hight": 3,
                    "text": "bold"
                }
            },
            {
                selector: 'edge',
                style: {
                    'curve-style': 'bezier',
                    'target-arrow-shape': 'triangle',
                    'line-color': 'pink'
                }
            },

            // some style for the extension

            {
                selector: '.eh-handle',
                style: {
                    'background-color': 'red',
                    'width': 12,
                    'height': 12,
                    'shape': 'ellipse',
                    'overlay-opacity': 0,
                    'border-width': 12, // makes the handle easier to hit
                    'border-opacity': 0
                }
            },

            {
                selector: '.eh-hover',
                style: {
                    'background-color': 'red'
                }
            },

            {
                selector: '.eh-source',
                style: {
                    'border-width': 2,
                    'border-color': 'red'
                }
            },

            {
                selector: '.eh-target',
                style: {
                    'border-width': 2,
                    'border-color': 'red'
                }
            },

            {
                selector: '.eh-preview, .eh-ghost-edge',
                style: {
                    'background-color': 'red',
                    'line-color': 'red',
                    'target-arrow-color': 'red',
                    'source-arrow-color': 'red'
                }
            },

            {
                selector: '.eh-ghost-edge.eh-preview-active',
                style: {
                    'opacity': 0
                }
            },
            {
                "selector": ".background",
                "style": {
                    "text-background-opacity": 1,
                    "color": "#fff",
                    "text-background-color": "#888",
                    "text-background-shape": "roundrectangle",
                    "text-border-color": "#000",
                    "text-border-width": 1,
                    "text-border-opacity": 1,
                    "font-size": "6px"
                }
            },
        ],

        elements: {
            nodes: nodes,
            edges: edges
        }
    });

    /*var eh = cy.edgehandles();*/
}