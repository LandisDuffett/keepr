<template>
  <div class="home text-center">
    <!--Modal-->
    <div
      class="modal fade"
      id="add-modal"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h3 class="modal-title">Add to Vault</h3>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <h5>Click button to add keep to that vault</h5>
              <div v-for="vault in vaults" :key="vault.id">
                <button
                  @click="addVaultkeep(vault.id)"
                  class="btn col-6 border rounded btn-info"
                >{{vault.name}}</button>
              </div>
              <div>
                <button
                  type="button"
                  class="col-10 m-2 btn rounded border btn-secondary"
                  data-dismiss="modal"
                >Close</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--Modal End-->
    <h3>Your Keeps</h3>
    <div class="flex-wrap d-flex">
      <div v-for="userKeep in userKeeps" :key="userKeep.id">
        <div class="bg-light border rounded border-info shadow m-3 px-4 py-3">
          <div class="row justify-content-center">
            <h3>{{userKeep.name}}</h3>
          </div>
          <div class="row">{{userKeep.description}}</div>
          <img :src="userKeep.img" alt style="max-width: 20rem" />
          <div class="row justify-content-center mt-2 mb-1">
            <button
              @click="deleteUserKeep(userKeep.id)"
              class="btn rounded border shadow btn-danger"
            >Delete</button>
            <button @click="addVaults(userKeep.id)" class="btn btn-sm border rounded btn-info">Keep</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "userKeeps",
  mounted() {
    this.$store.dispatch("getUserKeeps");
  },
  data() {
    return {
      newVaultkeep: {
        keepId: 0,
        vaultId: 0,
      },
    };
  },
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    },
    vaults() {
      return this.$store.state.vaults;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    deleteUserKeep(keepid) {
      this.$store.dispatch("deleteKeep", keepid);
    },
    async addVaults(data) {
      await this.$store.dispatch("getVaults");
      this.newVaultkeep.keepId = data;
      $("#add-modal").modal("show");
    },
    addVaultkeep(data) {
      this.newVaultkeep.vaultId = data;
      this.$store.dispatch("addVaultkeep", this.newVaultkeep);
    },
  },
};
</script>

<style></style>