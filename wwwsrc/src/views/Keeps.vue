<template>
  <div class="home text-center">
    <div class="row justify-content-center mt-2">
      <h5>Your Keeps</h5>
    </div>
    <div class="blogs row justify-content-center">
      <button
        type="button"
        class="btn m-1 btn-primary rounded border border-black shadow btn-lg"
        data-toggle="modal"
        data-target="#keep-modal"
        style="width: 25rem"
      >Create Keep</button>
    </div>
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
    <div
      class="modal fade"
      id="keep-modal"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Create Keep</h5>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <form @submit.prevent="addKeep">
                <div class="form-group row">
                  <label for="name" class="col-sm-1-12 col-form-label mr-2">Name:</label>
                  <div class="col-sm-1-12">
                    <input
                      type="text"
                      class="form-control"
                      name="name"
                      id="name"
                      placeholder="Enter keep title here..."
                      v-model="newKeep.name"
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
                      v-model="newKeep.description"
                      required
                    />
                  </div>
                </div>
                <div class="form-group row">
                  <label for="image" class="col-sm-1-12 col-form-label mr-2">Image:</label>
                  <div class="col-sm-1-12">
                    <input
                      type="text"
                      class="form-control"
                      name="image"
                      id="image"
                      placeholder="Enter an image URL..."
                      v-model="newKeep.img"
                    />
                  </div>
                </div>
                <div class="form-group row align-items-center">
                  <input
                    type="checkbox"
                    id="checkbox"
                    name="public"
                    @click="newKeep.isPrivate=!newKeep.isPrivate"
                  />
                  <span class="ml-2">Make Public</span>
                  <label for="checkbox"></label>
                </div>
                <div class="row ml-1">
                  <button type="submit" class="btn rounded shadow btn-primary">Submit Keep</button>
                </div>
                <div class="form-group row">
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn rounded shadow btn-secondary"
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
    <div class="flex-wrap d-flex">
      <div v-for="userKeep in userKeeps" :key="userKeep.id">
        <div
          class="bg-light border rounded border-info shadow m-3 px-4 py-3"
          style="max-width: 20rem;"
        >
          <div class="row justify-content-center">
            <h3>{{userKeep.name}}</h3>
          </div>
          <div class="row">{{userKeep.description}}</div>
          <div class="mb-1">
            <img :src="userKeep.img" alt style="max-width: 16rem" />
          </div>
          <div class="row border-bottom border-dark" style="border-width: 5px;"></div>
          <div class="row justify-content-center mt-2 mb-1">
            <button
              @click="addVaults(userKeep.id)"
              class="btn btn-sm border rounded btn-primary"
            >Keep</button>
            <button
              @click="deleteUserKeep(userKeep.id)"
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
      newKeep: {
        isPrivate: true,
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
    async addKeep() {
      console.log("hello");
      await this.$store.dispatch("addKeep", this.newKeep);
      $("#keep-modal").modal("hide");
    },
  },
};
</script>

<style></style>