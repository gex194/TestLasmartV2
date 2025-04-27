export async function updatePointPosition(pointValue, stage) {
    const pointerPosition = stage.getPointerPosition();
    const point = {x: pointerPosition.x, y: pointerPosition.y};
    await fetch(`/api/Point/${pointValue.id}`, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(point),
    })
}

export async function deletePoint(id, stage, layer) {
    await fetch(`/api/Point/${id}`, {method: 'DELETE'});
    const group = stage.find(`#group-${id}`)[0];
    group.destroy();
    layer.draw();
}

export async function updatePoint(pointValue, stage, layer) {
    const pointRadius = prompt('Enter point radius');
    const pointColor = prompt('Enter point color');
    const point = {x: pointValue.x, y: pointValue.y, radius: pointRadius, color: pointColor};
    await fetch(`/api/Point/${pointValue.id}`, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(point),
    })
    const pointStage = stage.find(`#point-${pointValue.id}`)[0];
    pointStage.radius(pointRadius);
    pointStage.fill(pointColor);
    layer.draw();
}