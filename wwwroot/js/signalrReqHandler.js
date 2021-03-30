var connection = new signalR.HubConnectionBuilder()
    .withUrl('/messages/im')
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('Send', message);
}