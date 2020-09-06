<template>
  <div>
    <h5>Welcome to Your Keeps</h5>
    <div v-for="userKeep in userKeeps" :key="userKeep.id">
      {{userKeep.name}} - {{userKeep.description}}
      <img
        :src="userKeep.img"
        alt
        style="max-width: 15rem; max-height:15rem"
      />
      <button @click="deleteUserKeep(userKeep.id)" class="btn btn-danger">Delete</button>
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