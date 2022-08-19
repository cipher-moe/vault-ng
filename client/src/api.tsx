import type { Replay } from './interface'
const basePath = '/api';

export enum Sort { Timestamp, Score, MaxCombo, Miss }
export enum SortDirection { Ascending, Descending }

const api = {
    replay: async (order = Sort.Timestamp,  direction = SortDirection.Descending) => {
        return await fetch(basePath + `/replays/?order=${order}&sortDirection=${direction}&count=40`).then(res => res.json()) as Replay[];
    }
}

export default api;