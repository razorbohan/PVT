document.getElementById("list").onclick = () => {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'participants.html', false);
    xhr.send();

    if (xhr.status === 200) {
        document.write(xhr.responseText);
    }
};

document.getElementById("sendMessage").onclick = function () {
    var text = document.getElementById("socket-text").value;
    socket.send(text);
}

var socket = new WebSocket("ws://localhost:8881");
socket.onmessage = function (event) {
    var message = document.createElement("div");
    message.innerHTML = event.data;
    document.getElementById("socket-area").appendChild(message);
};
socket.onopen = function () {
    //socket.send("Привет!");
}
socket.onclose = function (event) {
    if (event.wasClean) {
        console.log('Соединение закрыто чисто');
    } else {
        console.log('Обрыв соединения');
    }
    console.log('Код: ' + event.code + ' причина: ' + event.reason);
};
socket.onerror = function (error) {
    console.log("Ошибка " + error);
};