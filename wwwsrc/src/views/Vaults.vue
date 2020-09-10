<template>
  <div>
    <div class="row justify-content-center">
      <h5>Your Vaults</h5>
    </div>
    <div class="flex-wrap d-flex">
      <div v-for="vault in vaults" :key="vault.id">
        <div class="bg-light border rounded border-info shadow m-3 px-4 py-3">
          <div class="row justify-content-center">
            <router-link :to="{name: 'vaultkeeps', params: {vaultId: vault.id}}">
              <h4>{{vault.name}}</h4>
            </router-link>
          </div>
          <div class="row">{{vault.description}}</div>
          <div class="row justify-content-center">
            <button
              @click="deleteVault(vault.id)"
              class="btn border rounded shadow btn-danger mt-2"
            >Delete</button>
          </div>
        </div>
      </div>
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