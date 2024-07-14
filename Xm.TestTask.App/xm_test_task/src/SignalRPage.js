import { createSignalRContext } from "react-signalr/signalr";

const SignalRContext = createSignalRContext();

const SignalRPage = () => {
    async function invokeAction() {
        const actionType = "action";
        const message = JSON.stringify({ Name: "Test" });
        const response = await SignalRContext.invoke("SendMessage", actionType, message);
        console.log(response);
    }

    async function invokeNotification() {
        const actionType = "notification";
        const message = JSON.stringify({ Message: "TestMessage" });
        const response = await SignalRContext.invoke("SendMessage", actionType, message);
        console.log(response);
    }

    return (
        <SignalRContext.Provider
            url={"https://localhost:7142/eventHub"}
            onOpen={() => console.log("open")}
            onClosed={() => console.log("close", SignalRContext.connection?.state)}
        >
            <button onClick={() => invokeAction()}>Invoke Action signalR</button> <br /><br /><br />

            <button onClick={() => invokeNotification()}>Invoke Notification signalR</button>

        </SignalRContext.Provider>
    );
};

export default SignalRPage;