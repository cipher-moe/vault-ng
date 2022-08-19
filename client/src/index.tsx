import ReactDOM from 'react-dom';
import App from './App';
import './index.css';
import Background from "./background/Background";

ReactDOM.render((
    <>
        <div className="background-canvas">
            <Background />
        </div>
        <div className="z-0">
            <App />
        </div>
    </>
), document.querySelector('#root'));