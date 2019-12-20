<template>
  <div class="card">
     <div role="tablist" class="text-center">
      <b-card no-body bg-variant="dark" class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block href="#" v-b-toggle.accordion-1 variant="outline-success">Welcome To Your Profile Page!</b-button>
        </b-card-header>
        <b-collapse id="accordion-1" visible accordion="my-accordion" role="tabpanel">
          <b-card-body>

            <b-card-text class="text-white" v-if="currentUser">
              {{currentUser.firstName}} {{currentUser.surname}}
            </b-card-text>
          </b-card-body>
        </b-collapse>
      </b-card>

      <b-card no-body bg-variant="dark" class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block href="#" v-b-toggle.accordion-2 variant="outline-warning">You Can Check Your Username!</b-button>
        </b-card-header>
        <b-collapse id="accordion-2" accordion="my-accordion" role="tabpanel">
          <b-card-body>
            <b-card-text class="text-white" v-if="currentUser">{{currentUser.username}}</b-card-text>
          </b-card-body>
        </b-collapse>
      </b-card>

      <b-card no-body bg-variant="dark" class="mb-3">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block href="#" v-b-toggle.accordion-3 variant="outline-danger">And Don't Forget To Check Your Events!</b-button>
        </b-card-header>
        <b-collapse id="accordion-3" accordion="my-accordion" role="tabpanel">
          <b-card-body>
              <MyEvents/>
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

import MyEvents from '../views/MyEvents';

export default {
  name: "Profile",
  components:{
    MyEvents
  },
  data() {
    return {
      currentUser: JSON.parse(authenticationService.currentUserValue),
    };
  }
};
</script>
