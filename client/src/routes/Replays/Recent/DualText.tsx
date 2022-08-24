import {useState} from "react";

function DualText({ replace, original } : { replace: string, original: string }) {
    let [replaced, setReplaced] = useState(false);
    return (
        <div onMouseEnter={() => setReplaced(true)} onMouseLeave={() => setReplaced(false)}>
            {replaced ? replace : (original || replace)}
        </div>
    )
}

export default DualText;