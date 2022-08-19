import Recent from './routes/Recent';
import {BrowserRouter, Route, Routes} from "react-router-dom";
function App() {
    return (
        <div>
            <header>
                <nav className="bg-gray-800">
                    <div className="container max-w-7xl">
                        <img src="/vault-icon.svg" className="pl-4 h-14 svg-filter-white"  alt="vault" />
                    </div>
                </nav>
            </header>
            <BrowserRouter>
                <Routes>
                    <Route path="/recent" element={<Recent />} />
                </Routes>
            </BrowserRouter>
        </div>
    )
}

export default App;