<template>
  <div class="home">
    <h1>Welcome to Keepr!</h1>
    <div class="keeps row justify-content-center">
      <!-- Button trigger modal -->
      <button
        type="button"
        class="col-4 my-3 btn btn-primary btn-lg"
        data-toggle="modal"
        data-target="#keep-modal"
      >Create Keep</button>
    </div>
    <h5>create your own keep or put any keep below into a vault</h5>
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
                    :value="0"
                    v-model="newKeep.isPrivate"
                  />
                  <span class="ml-2">publish:</span>
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
            <h5 class="modal-title">Add to Vault</h5>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <h5>Click button to add keep to that vault</h5>
              <div v-for="vault in vaults" :key="vault.id">
                <button @click="addToVault(vault.id)" class="btn btn-danger">{{vault.name}}</button>
              </div>
              <div class="offset-sm-2 col-sm-10">
                <button type="submit" class="btn btn-primary">Add</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--Modal End-->
    <!--Modal-->
    <div
      class="modal fade"
      id="view-modal"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Keepviewer</h5>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <div>
                {{activeKeep.name}} - {{activeKeep.description}} - keeps: {{activeKeep.keeps}} views: {{activeKeep.views}} shares: {{activeKeep.shares}}
                <img
                  :src="activeKeep.img"
                  alt
                  style="max-width: 15rem; max-height:15rem"
                />
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--Modal End-->
    <div v-for="keep in keeps" :key="keep.id">
      {{keep.name}} - {{keep.description}} - keeps: {{keep.keeps}} views: {{keep.views}} shares: {{keep.shares}}
      <img
        :src="keep.img"
        alt
        style="max-width: 15rem; max-height:15rem"
      />
      <button @click="addVaults(keep.id)" class="btn btn-danger">Add to Vault</button>
      <button @click="viewModal(keep)" class="btn btn-danger">View Keep</button>
      <button @click="shareKeep(keep)" class="btn btn-danger">Share</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "keeps",
  name: "vaults",
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  data() {
    return {
      newKeep: {
        isPrivate: 0,
      },
      newVaultkeep: {
        keepId: 0,
        vaultId: 0,
      },
    };
  },
  computed: {
    keeps() {
      return this.$store.state.publicKeeps;
    },
    vaults() {
      return this.$store.state.vaults;
    },
    vaultkeeps() {
      return this.$store.state.vaultkeeps;
    },
    activeKeep() {
      return this.$store.state.activeKeep;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    addKeep() {
      console.log("hello");
      this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = {};
      $("#keep-modal").modal("hide");
    },
    async addToVault(data) {
      await this.$store.dispatch("getVaultkeeps", data);
      this.newVaultkeep.vaultId = data;
      let alreadyExist = this.vaultkeeps.some(
        (v) => v.id == this.newVaultkeep.keepId
      );
      if (!alreadyExist) {
        this.$store.dispatch("addVaultkeep", this.newVaultkeep);
        this.addKeptCount();
      } else {
        alert("You've already added this keep to this vault.");
      }
    },
    addKeptCount() {
      let found = this.keeps.find((k) => k.id == this.newVaultkeep.keepId);
      this.$store.dispatch("updateKeep", {
        id: found.id,
        keeps: found.keeps + 1,
      });
    },
    async viewModal(keep) {
      await this.$store.dispatch("setActiveKeep", keep);
      this.$store.dispatch("updateKeep", {
        id: keep.id,
        views: keep.views + 1,
      });
      $("#view-modal").modal("show");
    },
    async shareKeep(keep) {
      await this.$store.dispatch("updateKeep", {
        id: keep.id,
        shares: keep.shares + 1,
      });
    },
    async addVaults(data) {
      if (!this.$auth.user) {
        await this.$auth.loginWithPopup();
        await this.$store.dispatch("setBearer", this.$auth.bearer);
      }
      if (this.$auth.user) {
        await this.$store.dispatch("getVaults");
        this.newVaultkeep.keepId = data;
        $("#add-modal").modal("show");
      } else {
        this.$store.dispatch("getVaults");
      }
    },
  },
};
</script>


