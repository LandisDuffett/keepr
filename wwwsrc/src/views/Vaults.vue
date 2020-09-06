<template>
  <div>
    <h5>Welcome to Vaults</h5>
    <div v-for="vault in vaults" :key="vault.id">
      {{vault.name}} - {{vault.description}}
      <button
        @click="deleteVault(vault.id)"
        class="btn btn-danger"
      >Delete</button>
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