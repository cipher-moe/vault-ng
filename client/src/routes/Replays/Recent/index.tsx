import {useEffect, useLayoutEffect, useState} from 'react';
import type { Replay } from '../../../interface';
import api from '../../../api';
import ReplayCard from "./ReplayCard";
import {InView} from "react-intersection-observer";

let loadingTextClass = "text-white text-center font-bold text-lg";

function Recent() {
    let [replays, setReplays] = useState<Replay[]>([]);
    let [ended, setEnded] = useState(false);
    let [page, setPage] = useState(0);
    let [loading, setLoading] = useState(false);
    let [scrollY, setScrollY] = useState(window.scrollY);
    
    let load = () => {
        setLoading(true);
        api.replay(page)
            .then(res => {
                setScrollY(window.scrollY);
                setReplays(replays.concat(res));
                setPage(page + 1);
                setLoading(false);
                setEnded(!res.length);
            });
    }
    
    // eslint-disable-next-line react-hooks/exhaustive-deps
    useEffect(load, []);
    useLayoutEffect(() => {
        window.scroll({ top: scrollY });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [replays]);

    return (
        <>
            {!replays.length && loading && (
                <div className={loadingTextClass + " pt-4"}>
                    Please wait...
                </div>
            )}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8 py-4 px-6">
                {replays.map(r => {
                    return <ReplayCard r={r} key={r.sha256} />
                })}
            </div>
            {!ended && !!replays.length && (
                <InView onChange={(inView) => {
                    if (inView && !loading) {
                        load();
                    }
                }}>
                    <div className={loadingTextClass}>
                        Loading more...
                    </div>
                </InView>
            )}
        </>
    )
}

export default Recent;