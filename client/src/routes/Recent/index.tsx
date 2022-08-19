import { useEffect, useState } from 'react';
import type { Replay } from '../../interface';
import api from '../../api';
import ReplayCard from "./ReplayCard";

function Recent() {
    let [replays, setReplays] = useState<Replay[]>([]);
    useEffect(() => {
        api.replay()
            .then(res => setReplays(res))
    }, [])

    return (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8 py-4 px-6">
            {replays.map(r => {
                return <ReplayCard r={r} key={r.sha256} />
            })}
        </div>
    )
}

export default Recent;