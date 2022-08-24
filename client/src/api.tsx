import type { Replay } from './interface'
const basePath = '/api';

export enum Sort { Timestamp, Score, MaxCombo, Miss }
export enum SortDirection { Ascending, Descending }

const api = {
    replay: async (page = 0, count = 40, order = Sort.Timestamp, direction = SortDirection.Descending) => {
        return await fetch(basePath + `/replays/?order=${order}&sortDirection=${direction}&count=${count}&page=${page}`).then(res => res.json()) as Replay[];
    }
}

export default api;