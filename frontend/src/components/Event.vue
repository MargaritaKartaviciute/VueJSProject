<template>
  <div>
    <b-alert v-model="showDismissibleAlert" variant="danger" dismissible>Probably this event already in your list</b-alert>
    <b-alert v-model="showDismissibleAlert2" variant="success" dismissible>You added event to list</b-alert>
    <div class="card text-white bg-dark mb-3">
      <div class="row no-gutters">
        <div class="col-md-4">
          <!-- <img v-bind:src="event.image" v-if="event" class=" img-fluid card-img-top" alt="image" /> -->
          <b-img
            v-if="event && event.image"
            v-bind:src="event.image"
            class="img-fluid card-img-top thumbnail"
            fluid
            thumbnail
            alt="image"
            rounded
            style="max-width: 600px;max-height: 600px;"
          ></b-img>
          <b-img
            v-else
            src="https://www.ccjays.com/wp-content/uploads/2019/01/1969/always-up-to-date-guide-to-social-media-image-sizes.png"
            class="img-fluid card-img-top thumbnail"
            fluid
            thumbnail
            style="max-width: 600px;max-height: 600px;"
            alt="image"
            rounded
          ></b-img>
        </div>
        <div class="col-md-6">
          <div class="card-body">
            <h5 class="card-title" v-if="event">{{event.name}}</h5>
            <p class="card-text" v-if="event">{{event.description}}</p>
          </div>
        </div>
        <div class="col-md-2" v-if="currentUser">
          <a
            class="nav-link btn btn-outline-primary"
            v-on:click="putEvent(currentUser.id, event.id)"
          >Add to your list</a>
        </div>
      </div>
    </div>

    <div role="tablist" class="text-center">
      <b-card no-body bg-variant="dark" class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block href="#" v-b-toggle.accordion-1 variant="outline-success">The address</b-button>
        </b-card-header>
        <b-collapse id="accordion-1" visible accordion="my-accordion" role="tabpanel">
          <b-card-body>
            <b-card-text class="text-white" v-if="event && event.place">
              {{event.place.street}}
              {{event.place.number}},
              {{event.place.city}}
            </b-card-text>
          </b-card-body>
        </b-collapse>
      </b-card>

      <b-card no-body bg-variant="dark" class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block href="#" v-b-toggle.accordion-2 variant="outline-warning">Date</b-button>
        </b-card-header>
        <b-collapse id="accordion-2" accordion="my-accordion" role="tabpanel">
          <b-card-body>
            <b-card-text class="text-white" v-if="event && event.date">{{event.date.slice(0,10)}}</b-card-text>
          </b-card-body>
        </b-collapse>
      </b-card>

      <b-card no-body bg-variant="dark" class="mb-3">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block href="#" v-b-toggle.accordion-3 variant="outline-danger">Price</b-button>
        </b-card-header>
        <b-collapse id="accordion-3" accordion="my-accordion" role="tabpanel">
          <b-card-body>
            <b-card-text class="text-white" v-if="event">{{event.price}}</b-card-text>
          </b-card-body>
        </b-collapse>
      </b-card>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import { userService } from "../_services/user.service";
import { authenticationService } from "../_services/authentication.service";
export default {
  name: "Event",
  props: ["id"],
  data() {
    return {
      event: {},
      currentUser: JSON.parse(authenticationService.currentUserValue),
      api: this.$root.$data,
      showDismissibleAlert: false,
      showDismissibleAlert2: false
    };
  },
  mounted() {},
  created() {
    axios
      .get(this.api.value + "/events/" + this.$route.params.id)
      .then(
        response => (this.event = response.data),
      )
      .catch(err => {
        console.log(err);
      });
  },
  methods: {
    putEvent(currentUser, eventId) {
      axios
        .post(
          this.api.value + "/userEvents/" + currentUser + "/event/" + eventId
        )
        .then(
          response => (
            console.log(response.data),
            this.showDismissibleAlert2 = true
          )
        )
        .catch(err => {
          this.showDismissibleAlert = true;
          console.log(err);
        });
    }
  }
};
</script>
<style scoped>
.thumbnail:hover {
  position: relative;
  top: -25px;
  left: -35px;
  width: 500px;
  height: auto;
  display: block;
  z-index: 999;
}
</style>