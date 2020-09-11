<template>
  <div>
    <div class="row justify-content-center mt-2">
      <h5>Your Vaults</h5>
    </div>
    <div class="blogs row justify-content-center">
      <button
        type="button"
        class="rounded m-1 border shadow border-black btn btn-info btn-lg"
        style="width: 25rem"
        data-toggle="modal"
        data-target="#vault-modal"
      >Create Vault</button>
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
          <div class="row border-bottom border-dark" style="border-width: 5px;"></div>
          <div class="row justify-content-center">
            <button
              @click="deleteVault(vault.id)"
              class="btn btn-sm border rounded shadow btn-danger mt-2"
            >Delete</button>
          </div>
        </div>
      </div>
    </div>
    <!--Modal-->
    <div
      class="modal fade"
      id="vault-modal"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Create Vault</h5>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <form @submit.prevent="addVault">
                <div class="form-group row">
                  <label for="name" class="col-sm-1-12 col-form-label mr-2">Name:</label>
                  <div class="col-sm-1-12">
                    <input
                      type="text"
                      class="form-control"
                      name="name"
                      id="name"
                      placeholder="Enter keep title here..."
                      v-model="newVault.name"
                      required
                    />
                  </div>
                </div>
                <div class="form-group row">
                  <label for="body" class="col-sm-1-12 col-form-label mr-2">Description:</label>
                  <div class="col-sm-1-12">
                    <input
                      type="text"
                      class="form-control"
                      name="body"
                      id="body"
                      placeholder="Enter any text..."
                      v-model="newVault.description"
                      required
                    />
                  </div>
                </div>
                <div class="offset-sm-2 col-sm-10">
                  <button type="submit" class="btn rounded shadow border btn-primary">Submit Vault</button>
                </div>
                <div class="form-group row">
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn rounded shadow border btn-secondary"
                      data-dismiss="modal"
                    >Close</button>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--Modal End-->
  </div>
</template>



          
<script>
export default {
  name: "vaults",
  mounted() {
    this.$store.dispatch("getVaults");
  },
  data() {
    return {
      newVault: {},
    };
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
    addVault() {
      console.log("hello");
      this.$store.dispatch("addVault", this.newVault);
      this.newVault = {};
      $("#vault-modal").modal("hide");
    },
  },
};
</script>

<style></style>