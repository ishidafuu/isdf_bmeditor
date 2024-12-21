<template>
  <div v-if="activeMotionIndex >= 0">Selected: {{ activeMotionIndex }}: {{ motions[activeMotionIndex].name }}
  </div>
  <div class="list-container">
    <ul class="list-box">
      <li v-for="(motion, index) in motions" :key="motion.name"
          @click="changeMotionIndex(index)"
          :class="{ 'selected': activeMotionIndex === index }">
        {{ index.toString().padStart(3, '0') }}: {{ motion.name }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()

    const activeMotionIndex = computed(() => store.state[props.storeName].activeMotionIndex)
    const motions = computed(() => store.state[props.storeName].motions)

    const changeMotionIndex = (index: number) => {
      store.commit(`${props.storeName}/changeMotionIndex`, index)
    }

    return {
      motions,
      changeMotionIndex,
      activeMotionIndex,
    }
  }
})
</script>

<style>

.list-container {
  display: flex;
  justify-content: center;
}

.list-box {
  height: 200px;
  width: 200px;
  overflow-y: auto;
  border: 1px solid #ccc;
  text-align: left;
}

.selected {
  background-color: #444;
}
</style>
