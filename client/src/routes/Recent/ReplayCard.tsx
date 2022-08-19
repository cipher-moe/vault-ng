import humanizeDuration from "humanize-duration";
import DualText from "./DualText";
import icon from "../../IconUrl";
import {Replay} from "../../interface";
import {useMemo} from "react";

function ReplayCard({ r } : { r: Replay }) {
    let { detail } = r.beatmap || {};
    return (
        <div className="bg-white/50 rounded-xl flex flex-col">
            <div className="text-black text-center">
                {humanizeDuration(
                    (+new Date()) - (+new Date(r.timestamp)),
                    {
                        largest: 3,
                        conjunction: " and ",
                        serialComma: false,
                        round: true
                    }
                )} ago
            </div>
            <div key={r.timestamp}
                 style={{
                     backgroundImage: `
                            linear-gradient(rgb(0,0,0,0.25), rgb(255,255,255,0.4)),
                            linear-gradient(rgb(0,0,0,0.3), rgb(0,0,0,0.3)), 
                            url('https://assets.ppy.sh/beatmaps/${r.beatmap?.beatmapsetId}/covers/cover@2x.jpg')`
                 }}
                 className={
                     "flex-grow" +
                     " flex flex-col rounded-lg drop-shadow-md pt-1 pb-0.5"
                     + (r.perfect_combo ? " text-yellow-300 border-2 border-yellow-500" : " text-white")
                 }>

                <div className="flex flex-row flex-grow justify-between px-2">
                    <div className="flex-grow">
                        <b className="text-lg w-full">
                            <DualText
                                replace={detail?.title || '\u00a0'}
                                original={detail?.titleUnicode || detail?.title || '\u00a0'} />
                        </b>
                        <div className="text-sm font-light">
                            {detail?.artistUnicode || detail?.artist || '\u00a0'}
                        </div>
                    </div>

                    <div className="flex flex-row items-center">
                        <img
                            className="h-8 w-8 ml-auto p-1 svg-filter-white"
                            src={`/mode-icons/${icon(r.mode)}.svg`}
                            alt="game mode" />
                    </div>
                </div>
                <div className="bg-black/60 px-2">
                    <div className={detail?.diffName ? "text-yellow-200" : "text-red-200"}>
                        {detail?.diffName || '(unknown difficulty)'}
                    </div>
                    <div className="flex flex-row justify-between">
                    </div>
                </div>

                <div className="px-2">
                    <div className="flex flex-row justify-between">
                        <div>
                            <b className="drop-shadow-lg">{r.max_combo}</b>
                            <span className="drop-shadow-lg font-light text-sm">x</span>
                        </div>
                        <div>
                            <b className="drop-shadow-lg">{r.accuracy.toFixed(2)}</b>
                            <span className="drop-shadow-lg font-light text-sm">%</span>
                        </div>
                    </div>
                    <div className="flex flex-row justify-between">
                        <div>
                            <b className="drop-shadow-lg">{r.score.toLocaleString('en-US')}</b>
                            <span className="drop-shadow-lg font-light text-sm">x</span>
                        </div>
                        <div>
                            <b className="items-end">
                                {r.mode === 3 && (
                                    <>
                                        <b
                                            className="px-1 rounded-full text-white"
                                            style={{
                                                background: `linear-gradient(
                                                            to right,
                                                            rgb(255, 0, 0, 0.5),
                                                            rgb(196, 167, 4, 0.5),
                                                            rgb(0, 205, 255, 0.5),
                                                            rgb(0, 0, 255, 0.5),
                                                            rgb(255, 0, 255, 0.5))`
                                            }}>
                                            {r.count_geki}
                                        </b>
                                        &nbsp;<span className="drop-shadow-lg font-light text-sm">/</span>&nbsp;
                                    </>
                                )}
                                <b className={(r.mode === 3 ? "bg-yellow-500/50" : "bg-blue-500/50") + " px-1 rounded-full"}>{r.count_300}</b>
                                &nbsp;<span className="drop-shadow-lg font-light text-sm">/</span>&nbsp;
                                {r.mode === 3 && (
                                    <>
                                        <b className="bg-green-500/50 px-1 rounded-full">{r.count_katsu}</b>
                                        &nbsp;<span className="drop-shadow-lg font-light text-sm">/</span>&nbsp;
                                    </>
                                )}
                                <b className={(r.mode === 3 ? "bg-blue-500/50" : "bg-green-500/50") + " px-1 rounded-full"}>{r.count_100}</b>
                                &nbsp;<span className="drop-shadow-lg font-light text-sm">/</span>&nbsp;
                                <b className={(r.mode === 3 ? "bg-neutral-600/50" : "bg-orange-500/50") + " px-1 rounded-full"}>
                                    {r.count_50}
                                </b>
                                &nbsp;<span className="drop-shadow-lg font-light text-sm">/</span>&nbsp;
                                <b className="bg-red-500/50 px-1 rounded-full">{r.count_miss}</b>
                            </b>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

function MemoizedReplayCard({ r } : { r: Replay }) {
    // eslint-disable-next-line react-hooks/exhaustive-deps
    return useMemo(() => <ReplayCard r={r}/>, [r.sha256]);
}

export default MemoizedReplayCard;