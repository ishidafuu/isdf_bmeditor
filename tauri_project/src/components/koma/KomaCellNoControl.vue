<template>
  <div>
    <button @click="changeCellNo(-1)">-</button>
    <span>CellNo (Cursor): {{ cellNo }}</span>
    <button @click="changeCellNo(1)">+</button>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed, onMounted, onBeforeUnmount, toRefs} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const store = useStore()
    const {storeName} = toRefs(props)
    const cellNo = computed(() => {
      return store.state[storeName.value].motions[store.state[storeName.value].activeMotionIndex].komas[store.state[storeName.value].activeKomaIndex].cellNo
    })

    const changeCellNo = (amount: number) => {
      store.commit(`${storeName.value}/changeCellNo`, amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.shiftKey) {
        return;
      }

      if (event.code === 'ArrowLeft') {
        changeCellNo(-1);
      } else if (event.code === 'ArrowRight') {
        changeCellNo(1);
      } else if (event.code === 'ArrowUp') {
        changeCellNo(10);
      } else if (event.code === 'ArrowDown') {
        changeCellNo(-10);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      cellNo,
      changeCellNo
    }
  }
})
</script>
