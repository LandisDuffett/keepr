<template>
  <div class="home text-center">
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
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    deleteUserKeep(keepid) {
      this.$store.dispatch("deleteKeep", keepid);
    },
  },
};
</script>

<style></style>