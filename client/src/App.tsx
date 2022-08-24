import {BrowserRouter, Route, Routes} from "react-router-dom";
import {RoutePaths} from "./routes/RoutePaths";
import Replays from "./routes/Replays";
function App() {
    return (
        <div>
            <BrowserRouter>
                <header>
                    <nav className="bg-gray-800">
                        <div className="container max-w-7xl">
                            <img src="/vault-icon.svg" className="pl-4 h-14 svg-filter-white"  alt="vault" />
                        </div>
                    </nav>
                </header>
                <Routes>
                    <Route path={RoutePaths.Replays + '/*'} element={<Replays />}></Route>
                </Routes>
            </BrowserRouter>
        </div>
    )
}

export default App;