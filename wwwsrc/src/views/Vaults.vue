<template>
  <div>
    <h5>Welcome to Vaults</h5>
    <div v-for="vault in vaults" :key="vault.id">
      <router-link :to="{name: 'vaultkeeps', params: {vaultId: vault.id}}">{{vault.name}}</router-link>
      - {{vault.description}}
      <button @click="deleteVault(vault.id)" class="btn btn-danger">Delete</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "vaults",
  mounted() {
    this.$store.dispatch("getVaults");
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    deleteVault(vaultId) {
      this.$store.dispatch("deleteVault", vaultId);
    },
  },
};
</script>

<style></style>