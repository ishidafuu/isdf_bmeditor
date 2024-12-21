import {Cell} from '../types/interface.ts';
import {getFormattedDate} from './stringUtil.ts';
import {createDir, exists, readTextFile, writeFile} from "@tauri-apps/api/fs";
import {documentDir} from "@tauri-apps/api/path";

const initialCell: Cell = {
    bodyX: 0,
    bodyY: 0,
    faceIndex: 0,
    faceAngle: 0,
    faceX: 0,
    faceY: 0,
    faceZ: 0,
    itemAngle: 0,
    itemX: 0,
    itemY: 0,
    itemZ: 0,
};

const state = {
    cells: Array.from({length: 100}, () => ({...initialCell})),
    activeCellIndex: 0,
    editMode: 'Body',
    past: [],
    future: [],
};

function performPushToPast(state: any) {
    state.past.push(state.cells.map(cell => ({...cell})));
    state.future = []
}

function performClearPast(state: any) {
    state.past = [];
    state.future = [];
}


const mutations = {
    setEditMode(state, mode) {
        state.editMode = mode;
    },
    undo(state) {
        if (state.past.length > 0) {
            const previousState = state.past.pop();
            state.future.push({...state});
            Object.assign(state.cells, previousState);
        }
    },
    redo(state) {
        if (state.future.length > 0) {
            const nextState = state.future.pop();
            state.past.push({...state});
            Object.assign(state.cells, nextState);
        }
    },
    loadState(state, newState) {
        Object.assign(state, newState);
    },
    // 体パーツ
    changeCellIndex(state, amount) {
        performClearPast(state);
        const newIndex = state.activeCellIndex + amount;
        state.activeCellIndex = newIndex >= 0 ? newIndex : 0;
    },
    changeBodyX(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.bodyX += amount;
    },
    changeBodyY(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.bodyY += amount;
    },
    // 顔パーツ
    changeFaceIndex(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.faceIndex = (cell.faceIndex + 8 + amount) % 8;
    },
    changeFaceAngle(state, amount) {
        performPushToPast(state);
        cell.faceAngle = (cell.faceAngle + 4 + amount) % 4;
    },
    changeFaceX(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.faceX += amount;
    },
    changeFaceY(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.faceY += amount;
    },
    toggleFacePriority(state) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.faceZ = cell.faceZ === 0 ? -1 : 0;
    },
    // アイテム
    changeItemAngle(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.itemAngle = (cell.itemAngle + 8 + amount) % 8;
    },
    changeItemX(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.itemX += amount;
    },
    changeItemY(state, amount) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.itemY += amount;
    },
    toggleItemZ(state) {
        performPushToPast(state);
        const cell = state.cells[state.activeCellIndex];
        cell.itemZ = cell.itemZ === 0 ? -1 : 0;
    },
};

const actions = {
    async saveState({state, dispatch}) {
        try {
            const dataToSave = {
                cells: state.cells,
            };

            const saveDir = await dispatch('getSaveDir');
            if (!await exists(saveDir)) {
                await createDir(saveDir);
            }

            // Create a backup directory if it doesn't exist
            const backupDir = `${saveDir}/cell_data_backup`;
            if (!await exists(backupDir)) {
                await createDir(backupDir);
            }

            // Backup the current state
            const timestamp = getFormattedDate();
            const backupPath = `${backupDir}/cell_data_backup_${timestamp}.json`;
            await writeFile({path: backupPath, contents: JSON.stringify(dataToSave)});

            // Save the current state
            const path = `${saveDir}/cell_data.json`;
            await writeFile({path, contents: JSON.stringify(dataToSave)});
        } catch (error) {
            console.error('Failed to save state:', error);
        }
    },
    async loadState({commit, dispatch}) {
        try {
            const saveDir = await dispatch('getSaveDir');
            const path = `${saveDir}/cell_data.json`;

            const loadedState = JSON.parse(await readTextFile(path));
            commit('loadState', loadedState);
        } catch (error) {
            console.error('Failed to load state:', error);
        }
    },
    async getSaveDir() {
        return `${await documentDir()}/sai2_data`;
    },
};

const getters = {
    currentBodyState: (state: typeof state) => {
        return state.cells[state.activeCellIndex];
    },
};

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters,
};