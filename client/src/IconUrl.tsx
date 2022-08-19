 function iconName (mode : number) {
    switch (mode) {
        case 0: return 'osu';
        case 1: return 'taiko';
        case 2: return 'ctb';
        case 3: return 'mania';
    }
}

export default iconName;