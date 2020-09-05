<template>
  <div class="home">
    <h1>Welcome to Keepr!</h1>
    <div class="blogs row justify-content-center">
      <!-- Button trigger modal -->
      <button
        type="button"
        class="col-4 my-3 btn btn-primary btn-lg"
        data-toggle="modal"
        data-target="#keep-modal"
      >Create Keep</button>
    </div>
    <h5>create your own keep or put any keep below into your vault</h5>
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
                  <label for="name" class="col-sm-1-12 col-form-label">Keep name:</label>
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

                <div class="form-group row align-items-center">
                  <input type="checkbox" id="checkbox" name="public" v-model="newKeep.isPrivate"><span class="ml-2">Make public</span></input>
                  <label for="checkbox">{{checked}}</label>
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
  name: "home",
  data() {
    return {
      newKeep: {},
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
  },
};
</script>


