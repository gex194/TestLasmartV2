export async function addComment(pointId) {
    const commentText = prompt('Enter comment text');
    const commentBg = prompt('Enter comment bg color');
    const comment = {text: commentText, bgColor: commentBg, pointId: pointId}
    const response = await fetch('/api/Comment', {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(comment),
    })
}

export async function updateComment(commentValue, commentId, stage, layer) {
    const commentText = prompt('Enter comment text');
    const commentBg = prompt('Enter comment bg color');
    const comment = {text: commentText, bgColor: commentBg, pointId: commentValue.pointId}
    await fetch(`/api/Comment/${commentId}`, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(comment),
    })
    const commentStage = stage.find(`#comment-${commentId}`)[0];
    const commentBgStage = stage.find(`#comment-bg-${commentId}`)[0];
    commentStage.text(commentText);
    commentBgStage.fill(commentBg);
    layer.draw();
}