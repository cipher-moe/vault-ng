export interface BeatmapDetail {
    md5: string;
    sha256: string;
    length: number;
    drainLength: number;
    combo: number;
    titleUnicode: string;
    title: string;
    artistUnicode: string;
    artist: string;
    diffName: string;
    minBpm: number;
    maxBpm: number;
    countObjects: number;
}

export interface Beatmap {
    md5: string;
    beatmapId: string;
    beatmapsetId: string;
    date: string;
    detail?: BeatmapDetail;
}

export interface Replay {
    mode: number;
    version: number;
    beatmap_hash: string;
    player_name: string;
    replay_hash: string;
    count_300: number;
    count_100: number;
    count_50: number;
    count_geki: number;
    count_katsu: number;
    count_miss: number;
    score: number;
    max_combo: number;
    perfect_combo: boolean;
    mods: number;
    timestamp: string;
    sha256: string;
    accuracy: number;
    beatmap?: Beatmap;
}