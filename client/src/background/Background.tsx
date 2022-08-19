import Triangles from "./Triangles";
import {useEffect, useRef} from "react";
function Background () {
    let ref = useRef<HTMLCanvasElement>(null);
    
    useEffect(() => {
        let canvas = ref.current!;
        new Triangles(canvas,  "#595959", 150);
        window.addEventListener("resize", () => {
            canvas.width = document.body.clientWidth;
            canvas.height = window.visualViewport.height
        });
    }, [ref])
    
    return (
        <div>
            <canvas ref={ref} width={document.body.clientWidth} height={window.visualViewport.height}></canvas>
        </div>
    )
}

export default Background;