import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    user: {},
    publicKeeps: [],
    userKeeps: [],
    vaults: [],
    vaultkeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = publicKeeps
    },
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps
    },
    setVaultkeeps(state, vaultkeeps) {
      state.vaultkeeps = vaultkeeps
    }
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    async addKeep({ dispatch }, newKeep) {
      try {
        let res = await api.post('keeps', newKeep)
        dispatch('getPublicKeeps')
      } catch (error) {
        console.error(error)
      }
    },
    getPublicKeeps({ commit }) {
      api.get('keeps').then(res => {
        commit('setPublicKeeps', res.data)
      })
    },
    getUserKeeps({ commit }) {
      api.get('keeps/user').then(res => {
        commit('setUserKeeps', res.data)
      })
    },
    async addVault({ dispatch }, newVault) {
      try {
        let res = await api.post('vaults', newVault)
        dispatch('getVaults')
      } catch (error) {
        console.error(error)
      }
    },
    async deleteVault({ commit, dispatch }, vaultId) {
      try {
        let res = await api.delete("vaults/" + vaultId)
        dispatch("getVaults")
      } catch (error) {
        console.error(error)
      }
    },
    async deleteKeep({ commit, dispatch }, keepId) {
      try {
        let res = await api.delete("keeps/" + keepId)
        dispatch("getUserKeeps")
      } catch (error) {
        console.error(error)
      }
    },
    async getVaults({ commit }) {
      try {
        let res = await api.get('vaults')
        commit('setVaults', res.data)
      } catch (error) {
        console.error(error)
      }
      //api.get('vaults').then(res => {
      //commit('setVaults', res.data)
      //})
    },
    async addVaultkeep({ commit, dispatch }, newVk) {
      try {
        let res = await api.post('vaultkeeps', newVk)
      } catch (error) {
        console.error(error)
      }
    },
    async deleteVaultkeep({ commit, dispatch }, data) {
      try {
        let res = await api.delete("vaultkeeps/" + data.vaultkeepId)
        dispatch("getVaultkeeps", data.vaultId)
      } catch (error) {
        console.error(error)
      }
    },
    getVaultkeeps({ commit }, vaultId) {
      api.get('vaults/' + vaultId + "/keeps").then(res => {
        commit('setVaultkeeps', res.data)
      })
    },
  }
});
