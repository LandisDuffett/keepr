<template>
  <div class="dashboard">
    <h1>WELCOME TO VAULTKEEPS</h1>
    <div v-for="vaultkeep in vaultkeeps" :key="vaultkeep.id">
      {{vaultkeep.name}} - {{vaultkeep.description}}
      <img
        :src="vaultkeep.img"
        alt
        style="max-width: 15rem; max-height:15rem"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: "vaultkeeps",
  mounted() {
    this.$store.dispatch("getVaultkeeps", this.$route.params.vaultId);
  },
  computed: {
    vaultkeeps() {
      return this.$store.state.vaultkeeps;
    },
  },
};
</script>

<style></style>