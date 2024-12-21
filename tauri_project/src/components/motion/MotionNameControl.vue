<template>
  <span>MotionName </span>
  <input type="text" v-model="currentName" @input="changeMotionName(currentName)"/>
</template>

<script lang="ts">
import {defineComponent, computed, ref} from 'vue'
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
    const name = computed(() => store.state[props.storeName].motions[store.state[props.storeName].activeMotionIndex].name)
    const currentName = ref(name.value)
    const changeMotionName = (newName: string) => {
      store.commit(`${props.storeName}/changeMotionName`, newName)
    }

    return {
      changeMotionName,
      currentName,
    }
  }
})
</script>
