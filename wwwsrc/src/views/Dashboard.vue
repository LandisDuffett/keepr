<template>
  <div class="home">
    <h1 class="text-center">Your Dashboard!</h1>
    <div class="blogs row justify-content-center">
      <!-- Button trigger modal -->
      <router-link :to="{name: 'keeps'}">
        <button
          type="button"
          class="col-8 my-3 btn btn-primary rounded border shadow btn-lg"
        >Go to my keeps</button>
      </router-link>
      <button
        type="button"
        class="col-1 my-3 btn btn-primary rounded border shadow btn-lg"
        data-toggle="modal"
        data-target="#keep-modal"
      >Create Keep</button>
    </div>
    <!--Modal-->
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
                  <label for="name" class="col-sm-1-12 col-form-label">Name:</label>
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
                  <label for="body" class="col-sm-1-12 col-form-label">Description:</label>
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
                  <label for="image" class="col-sm-1-12 col-form-label">Image:</label>
                  <div class="col-sm-1-12">
                    <input
                      type="text"
                      class="form-control"
                      name="image"
                      id="image"
                      placeholder="Enter an image URL..."
                      v-model="newKeep.img"
                      required
                    />
                  </div>
                </div>
                <div class="form-group row align-items-center">
                  <input
                    type="checkbox"
                    id="checkbox"
                    name="public"
                    value="false"
                    v-model="newKeep.isPrivate"
                  />
                  <span class="ml-2">Private (uncheck to make public)</span>
                  <label for="checkbox"></label>
                </div>
                <div class="offset-sm-2 col-sm-10">
                  <button type="submit" class="btn btn-primary">Submit Keep</button>
                </div>
                <div class="form-group row">
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--Modal End-->
    <div class="keeps row justify-content-center">
      <!-- Button trigger modal -->
      <router-link :to="{name: 'vaults'}">
        <button
          type="button"
          class="col-8 my-3 btn btn-primary rounded border shadow btn-lg"
        >Go to my vaults</button>
      </router-link>
      <button
        type="button"
        class="col-1 rounded border shadow my-3 btn btn-primary btn-lg"
        data-toggle="modal"
        data-target="#vault-modal"
      >Create Vault</button>
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
                  <label for="name" class="col-sm-1-12 col-form-label">Name:</label>
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
                  <label for="body" class="col-sm-1-12 col-form-label">Description:</label>
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
                  <button type="submit" class="btn btn-primary">Submit Vault</button>
                </div>
                <div class="form-group row">
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
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
  name: "keeps",
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  data() {
    return {
      newKeep: {
        isPrivate: 1,
      },
      newVault: {},
    };
  },
  computed: {
    keeps() {
      return this.$store.state.publicKeeps;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    async addKeep() {
      console.log("hello");
      await this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = {
        isPrivate: 1,
      };
      $("#keep-modal").modal("hide");
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
