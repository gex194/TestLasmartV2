import {deletePoint, updatePoint, updatePointPosition} from './point.js';
import {addComment, updateComment} from "./comment.js";

const stage = new Konva.Stage({
    container: 'konva-stage',
    width: window.innerWidth,
    height: window.innerHeight
})

const layer = new Konva.Layer();
stage.add(layer);

async function fetchData() {
    layer.destroyChildren();
    const response = await fetch('/api/Point');
    const points = await response.json();
    
    points.forEach(point => {
        renderObjects(point);
    })
}

function renderObjects(point) {
    //Initializing shapes and objects
    const group = new Konva.Group({
        id: `group-${point.id}`,
        draggable: true,
    });
    
    const groupComments = new Konva.Group({
        id: `group-comments-${point.id}`,
        draggable: false,
    })
    const circle = new Konva.Circle({
        x: point.x,
        y: point.y,
        radius: point.radius,
        fill: point.color,
        id: `point-${point.id}`
    })
    const addCommentButton = new Konva.Text({
        x: circle.x() + point.radius + 10,
        y: circle.y() - circle.radius(),
        fill: '#555',
        text: 'Add comment',
        fontSize: 16,
        width: 100,
        height: 20,
    })
    const commentButtonBg = new Konva.Rect({
        x: addCommentButton.x() - 5,
        y: addCommentButton.y() - 5,
        width: addCommentButton.getWidth() + 10,
        height: addCommentButton.getHeight() + 5,
        stroke: "black",
        fill: '#fff',
    })
    
    if (point.comments && point.comments.length > 0) {
        let yOffset = circle.radius() + 20;
        point.comments.forEach(comment => {
            const commentText = new Konva.Text({
                x: point.x - circle.radius(),
                y: point.y + yOffset,
                text: comment.text,
                centerAlign: true,
                fontSize: 14,
                fill:'#000',
                id: `comment-${comment.id}`,
            });
            
            const commentBg = new Konva.Rect({
                x: point.x - circle.radius() - 10,
                y: point.y + yOffset - 10,
                centralAlign: true,
                borderBlock: 'solid 1px #000',
                width: commentText.getWidth() + 20,
                height: commentText.getHeight() + 20,
                stroke: 'black',
                fill: comment.bgColor || '#fff',
                id: `comment-bg-${comment.id}`,
            })
            
            yOffset += 38;
            groupComments.add(commentBg);
            groupComments.add(commentText);

            commentText.on('click', () => {
                updateComment(commentText, comment.id, stage, layer).then(() => fetchData())
            })
        })
    }
    
    //Adding events for controls
    circle.on('dblclick', () => deletePoint(point.id, stage, layer))
    circle.on('contextmenu', (e) => {
        e.cancelBubble = true;
        updatePoint(point, stage, layer).then(() => fetchData());
    })

    addCommentButton.on('click', () => {
        addComment(point.id).then(() => fetchData())
    })

    group.on('dragend', (t) => {
        updatePointPosition(point, stage);
    })
    groupComments.on('mousedown', (e) => {
        e.cancelBubble = true;
    })
    
    //Adding shapes and objects to stage
    groupComments.add(commentButtonBg);
    groupComments.add(addCommentButton);
    
    group.add(circle);
    group.add(groupComments);
    layer.add(group);
    layer.draw();
}

fetchData();

document.getElementById('add-point').addEventListener('click', () => {
    const x = Math.random() * stage.width();
    const y = Math.random() * stage.height();
    const radius = 20;
    const color = Konva.Util.getRandomColor();
    
    const point = {x, y, radius, color};
    
    fetch('/api/Point', {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(point),
    }).then(response => response.json())
        .then(newPoint => renderObjects(newPoint));
})