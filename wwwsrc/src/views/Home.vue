<template>
  <div class="home text-center">
    <h5 class="mt-1">
      <b>Welcome to keepr, the app that lets you post your interests and keep posts that others have made. View any post on this page, or log in to create your own vaults, make your own posts, and keep any post in your vaults.</b>
    </h5>
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
                  class="btn col-6 border rounded border-black btn-info"
                >{{vault.name}}</button>
              </div>
              <div>
                <button
                  type="button"
                  class="col-10 m-2 btn rounded border border-black btn-secondary"
                  data-dismiss="modal"
                >Close</button>
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
            <h5 class="modal-title"></h5>
          </div>
          <div class="modal-body">
            <div class="container-fluid">
              <div>
                <div class="row">
                  <h3 class="ml-5">{{activeKeep.name}}</h3>
                </div>
                <div class="row">
                  <h5>{{activeKeep.description}}</h5>
                </div>
                <div class="row m-2 border rounded shadow border-primary justify-content-center">
                  <img :src="activeKeep.img" alt style="max-width: 15rem; max-height:15rem" />
                </div>
                <div class="row">
                  <h5>
                    <span class="mr-3">keeps: {{activeKeep.keeps}}</span>
                    <span class="mr-3">views: {{activeKeep.views}}</span>
                    <span>shares: {{activeKeep.shares}}</span>
                  </h5>
                </div>
              </div>
              <div class="modal-footer">
                <button
                  type="button"
                  class="btn rounded border border-light shadow btn-secondary"
                  data-dismiss="modal"
                >Close</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--Modal End-->
    <div class="flex-wrap d-flex">
      <div v-for="keep in keeps" :key="keep.id">
        <div class="bg-light border rounded border-primary shadow m-3 px-4 py-3">
          <div class="row justify-content-center">
            <h3>{{keep.name}}</h3>
          </div>
          <div class="row">{{keep.description}}</div>
          <div class="row mb-1">
            <img :src="keep.img" alt style="max-width: 20rem" />
          </div>
          <div class="row border-bottom border-dark" style="border-width: 5px;"></div>
          <div class="row border border-solid rounded border-dark">
            <img :src="keep.img" alt style="max-width: 20rem" />
          </div>
          <div class="row justify-content-center mt-2 mb-1">
            <button @click="addVaults(keep.id)" class="btn btn-sm border rounded btn-info">Keep</button>
            <button @click="viewModal(keep)" class="btn btn-sm border rounded btn-info">View</button>
            <button @click="shareKeep(keep)" class="btn btn-sm border rounded btn-info">Share</button>
          </div>
          <div class="row justify-content-center">
            <span class="mr-2">
              <b>keeps:</b>
              {{keep.keeps}}
            </span>
            <span class="mr-2">
              <b>views:</b>
              {{keep.views}}
            </span>
            <span class="mr-2">
              <b>shares:</b>
              {{keep.shares}}
            </span>
          </div>
        </div>
      </div>
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
    async addVaultkeep(data) {
      this.newVaultkeep.vaultId = data;
      await this.$store.dispatch("addVaultkeep", this.newVaultkeep);
      this.addKeptCount();
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