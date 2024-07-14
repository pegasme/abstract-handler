import './App.css';
import SignalRPage from './SignalRPage';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
        </a>
      </header>
      <div>
        <h3>React signalR</h3>

        <SignalRPage />
      </div>
    </div>
  );
}

export default App;
