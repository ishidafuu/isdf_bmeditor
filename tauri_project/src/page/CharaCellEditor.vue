<template>
  <div class="base-motion-container">
    <div class="character-view">
      <CharacterView :activeCellIndex="activeCellIndex" :offsetX="0" :offsetY="0"/>
      <ErrorView/>
    </div>
    <div class="control-view">
      <CharaCellControlView/>
    </div>
  </div>
</template>

<script>
import CharacterView from "../components/common/CharacterView.vue";
import CharaCellControlView from "../components/chara-cell/CharaCellController.vue";
import {mapState} from "vuex";
import ErrorView from "../components/common/ErrorView.vue";

export default {
  name: 'CharaCellEditor',
  components: {
    ErrorView,
    CharacterView,
    CharaCellControlView,
  },
  computed: {
    ...mapState({
      activeCellIndex: state => state.charaCell.activeCellIndex,
    }),
  }
}
</script>

<style scoped>
.base-motion-container {
  display: flex; /* Flexboxを有効に */
}

.character-view {
  flex: 1; /* 左側の領域を均等に占める */
}

.control-view {
  flex: 1; /* 右側の領域を均等に占める */
  min-width: 600px;
}
</style>