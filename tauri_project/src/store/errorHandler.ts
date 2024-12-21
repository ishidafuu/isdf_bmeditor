const state = {
    latestError: '',
};

const mutations = {
    setError(state, error: String) {
        state.latestError = error;
    },
    clearError(state) {
        state.latestError = null;
    },
};

const actions = {
    // ここで非同期アクションを定義できます。
};

const getters = {
    getLatestError: (state: typeof state) => {
        return state.latestError;
    },
};

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters,
};