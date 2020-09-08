<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormularioAutomata.aspx.cs" Inherits="ProyectoAutomatas.FormularioAutomata" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <meta charset="UTF-8">
  <title>Concept Map</title>
  <meta name="description" content="A concept map diagram implemented with labeled links and ForceDirectedLayout." />
  <meta name="viewport" content="width=device-width, initial-scale=1">

  <script src="../release/go.js"></script>
  <script src="../assets/js/goSamples.js"></script>  
  <script id="code">
      function init() {
          if (window.goSamples) goSamples();  
          var $ = go.GraphObject.make;  

          var roundedRectangleParams = {
              parameter1: 2,  
              spot1: go.Spot.TopLeft, spot2: go.Spot.BottomRight  
          };

          myDiagram =
              $(go.Diagram, "myDiagramDiv",  
                  {
                      "animationManager.initialAnimationStyle": go.AnimationManager.None,
                      "InitialAnimationStarting": function (e) {
                          var animation = e.subject.defaultAnimation;
                          animation.easing = go.Animation.EaseOutExpo;
                          animation.duration = 900;
                          animation.add(e.diagram, 'scale', 0.1, 1);
                          animation.add(e.diagram, 'opacity', 0, 1);
                      },

                      "toolManager.mouseWheelBehavior": go.ToolManager.WheelZoom,
                      "clickCreatingTool.archetypeNodeData": { text: "new node" },
                      "undoManager.isEnabled": true,
                      positionComputation: function (diagram, pt) {
                          return new go.Point(Math.floor(pt.x), Math.floor(pt.y));
                      }
                  });

          myDiagram.addDiagramListener("Modified", function (e) {
              var button = document.getElementById("SaveButton");
              if (button) button.disabled = !myDiagram.isModified;
              var idx = document.title.indexOf("*");
              if (myDiagram.isModified) {
                  if (idx < 0) document.title += "*";
              } else {
                  if (idx >= 0) document.title = document.title.substr(0, idx);
              }
          });

          myDiagram.nodeTemplate =
              $(go.Node, "Auto",
                  {
                      locationSpot: go.Spot.Top,
                      isShadowed: true, shadowBlur: 1,
                      shadowOffset: new go.Point(0, 1),
                      shadowColor: "rgba(0, 0, 0, .14)"
                  },
                  new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify),
                  $(go.Shape, "RoundedRectangle", roundedRectangleParams,
                      {
                          name: "SHAPE", fill: "#ffffff", strokeWidth: 0,
                          stroke: null,
                          portId: "",  
                          fromLinkable: true, fromLinkableSelfNode: true, fromLinkableDuplicates: true,
                          toLinkable: true, toLinkableSelfNode: true, toLinkableDuplicates: true,
                          cursor: "pointer"
                      }),
                  $(go.TextBlock,
                      {
                          font: "bold small-caps 11pt helvetica, bold arial, sans-serif", margin: 7, stroke: "rgba(0, 0, 0, .87)",
                          editable: true 
                      },
                      new go.Binding("text").makeTwoWay())
              );

          myDiagram.nodeTemplate.selectionAdornmentTemplate =
              $(go.Adornment, "Spot",
                  $(go.Panel, "Auto",
                      $(go.Shape, "RoundedRectangle", roundedRectangleParams,
                          { fill: null, stroke: "#7986cb", strokeWidth: 3 }),
                      $(go.Placeholder) 
                  ),
                  $("Button",
                      {
                          alignment: go.Spot.TopRight,
                          click: addNodeAndLink  
                      },
                      $(go.Shape, "PlusLine", { width: 6, height: 6 })
                  ) 
              ); 

          myDiagram.nodeTemplateMap.add("Start",
              $(go.Node, "Spot", { desiredSize: new go.Size(75, 75) },
                  new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify),
                  $(go.Shape, "Circle",
                      {
                          fill: "#52ce60", /* green */
                          stroke: null,
                          portId: "",
                          fromLinkable: true, fromLinkableSelfNode: true, fromLinkableDuplicates: true,
                          toLinkable: true, toLinkableSelfNode: true, toLinkableDuplicates: true,
                          cursor: "pointer"
                      }),
                  $(go.TextBlock, "Start",
                      {
                          font: "bold 16pt helvetica, bold arial, sans-serif",
                          stroke: "whitesmoke"
                      })
              )
          );

          myDiagram.nodeTemplateMap.add("End",
              $(go.Node, "Spot", { desiredSize: new go.Size(75, 75) },
                  new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify),
                  $(go.Shape, "Circle",
                      {
                          fill: "maroon",
                          stroke: null,
                          portId: "",
                          fromLinkable: true, fromLinkableSelfNode: true, fromLinkableDuplicates: true,
                          toLinkable: true, toLinkableSelfNode: true, toLinkableDuplicates: true,
                          cursor: "pointer"
                      }),
                  $(go.Shape, "Circle", { fill: null, desiredSize: new go.Size(65, 65), strokeWidth: 2, stroke: "whitesmoke" }),
                  $(go.TextBlock, "End",
                      {
                          font: "bold 16pt helvetica, bold arial, sans-serif",
                          stroke: "whitesmoke"
                      })
              )
          );

          function addNodeAndLink(e, obj) {
              var adornment = obj.part;
              var diagram = e.diagram;
              diagram.startTransaction("Add State");

              var fromNode = adornment.adornedPart;
              var fromData = fromNode.data;
              var toData = { text: "new" };
              var p = fromNode.location.copy();
              p.x += 200;
              toData.loc = go.Point.stringify(p);  

              var model = diagram.model;
              model.addNodeData(toData);

              var linkdata = {
                  from: model.getKeyForNodeData(fromData),
                  to: model.getKeyForNodeData(toData),
                  text: "transition"
              };
              model.addLinkData(linkdata);
              var newnode = diagram.findNodeForData(toData);
              diagram.select(newnode);

              diagram.commitTransaction("Add State");
              diagram.scrollToRect(newnode.actualBounds);
          }

          myDiagram.linkTemplate =
              $(go.Link, 
                  {
                      curve: go.Link.Bezier,
                      adjusting: go.Link.Stretch,
                      reshapable: true, relinkableFrom: true, relinkableTo: true,
                      toShortLength: 3
                  },
                  new go.Binding("points").makeTwoWay(),
                  new go.Binding("curviness"),
                  $(go.Shape,  
                      { strokeWidth: 1.5 },
                      new go.Binding('stroke', 'progress', function (progress) {
                          return progress ? "#52ce60" /* green */ : 'black';
                      }),
                      new go.Binding('strokeWidth', 'progress', function (progress) {
                          return progress ? 2.5 : 1.5;
                      })
                  ),
                  $(go.Shape,  
                      { toArrow: "standard", stroke: null },
                      new go.Binding('fill', 'progress', function (progress) {
                          return progress ? "#52ce60" /* green */ : 'black';
                      })),
                  $(go.Panel, "Auto",
                      $(go.Shape,  
                          {
                              fill: $(go.Brush, "Radial",
                                  { 0: "rgb(245, 245, 245)", 0.7: "rgb(245, 245, 245)", 1: "rgba(245, 245, 245, 0)" }),
                              stroke: null
                          }),
                      $(go.TextBlock, "transition", 
                          {
                              textAlign: "center",
                              font: "9pt helvetica, arial, sans-serif",
                              margin: 4,
                              editable: true  
                          },
                          new go.Binding("text").makeTwoWay())
                  )
              );

          load();
      }

      function save() {
          document.getElementById("mySavedModel").value = myDiagram.model.toJson();
      }
      function load() {
          myDiagram.model = go.Model.fromJson(document.getElementById("mySavedModel").value);
      }
  </script>
</head>
<body  onload="init()">
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TxtCadena" runat="server"></asp:TextBox>
            <asp:Button ID="BtnEvaluaCadena" Text="Evaluar Cadena" OnClick="BtnEvaluaCadena_Click" runat="server" />

            <br />
 
            <asp:TextBox runat="server" ID="TxtResultado" TextMode="MultiLine" Rows="10" />
             <br />
             <asp:TextBox runat="server" ID="TxtCadenas" TextMode="MultiLine" Rows="10" />

        </div>
<div id="sample">
    <div id="myDiagramDiv" style="border: solid 1px black; width: 100%; height: 460px; background: whitesmoke"></div>
    <div>
    <textarea visible="true" id="mySavedModel" runat="server" style="width:100%;height:300px">
        { "class": "go.GraphLinksModel",
          "nodeKeyProperty": "id",
          "nodeDataArray": [
          {"id":1, "loc":"150 0", "text":"q0"},{"id":2, "loc":"300 0", "text":"q1"},{"id":3, "loc":"450 0",  "text":"q2"},
          {"id":4, "loc":"600 0", "text": "q3"},{"id":5, "loc":"750 0", "text": "q4"}
          ],
          "linkDataArray": [
            { "from": 1, "to": 2, "text": "0" },
            { "from": 2, "to": 3,   "text": "1" },
            { "from": 3, "to": 4,   "text": "0" },
            { "from": 4, "to": 5,   "text": "1" },
            { "from": 5, "to": 5,   "text": "1" },
            { "from": 4, "to": 4,   "text": "0" }
          ]
        }
    </textarea>
    </div>
  </div>
    </form>
</body>
</html>
