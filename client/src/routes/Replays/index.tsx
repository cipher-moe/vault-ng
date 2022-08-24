import {Route, Routes} from "react-router-dom";
import {Paths} from "./paths";
import Recent from "./Recent";

function Replays() {
    return (
        <Routes>
            <Route path={Paths.Recent} element={<Recent />} />
        </Routes>
    )
}

export default Replays;