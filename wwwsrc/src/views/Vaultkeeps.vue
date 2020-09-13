<template>
  <div class="vaultkeeps">
    <div class="text-center">
      <h3 class="justify-content-center mt-2">Vault Name: {{vaultname}}</h3>
    </div>
    <div class="flex-wrap d-flex">
      <div v-for="vaultkeep in vaultkeeps" :key="vaultkeep.id">
        <div
          class="bg-light border rounded border-info shadow m-3 px-4 py-3"
          style="max-width: 25rem;"
        >
          <div class="row justify-content-center">
            <h3>{{vaultkeep.name}}</h3>
          </div>
          <div class="row">{{vaultkeep.description}}</div>
          <div class="mb-1 ml-3">
            <img :src="vaultkeep.img" alt style="max-width: 20rem" />
          </div>
          <div class="row border-bottom border-dark" style="border-width: 5px;"></div>
          <div class="row justify-content-center mt-2 mb-1">
            <button
              @click="deleteVaultkeep(vaultkeep.vaultKeepId)"
              class="btn rounded border btn-sm shadow btn-danger"
            >Delete</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "vaultkeeps",
  mounted() {
    this.$store.dispatch("getVaultkeeps", this.$route.params.vaultId);
    this.$store.dispatch("getVaults");
  },
  computed: {
    vaultkeeps() {
      return this.$store.state.vaultkeeps;
    },
    vaultname() {
      let vaults = this.$store.state.vaults;
      let found = vaults.find((v) => v.id == this.$route.params.vaultId);
      return found.name;
    },
  },
  methods: {
    deleteVaultkeep(data) {
      this.$store.dispatch("deleteVaultkeep", {
        vaultKeepId: data,
        vaultId: this.$route.params.vaultId,
      });
    },
  },
};
</script>

<style></style>